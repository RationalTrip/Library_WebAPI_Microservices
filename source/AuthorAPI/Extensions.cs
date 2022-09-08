using AuthorAPI.Repository;
using LibraryTransit.Contract.Configuration;
using MassTransit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AuthorAPI
{
    static class Extensions
    {
        public static void AddAuthorDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                string connectionString = configuration.GetConnectionString("AuthorsDb");
                services.AddDbContext<AuthorDbContext>(opt => opt.UseSqlServer(connectionString));
            }
            else
            {
                services.AddDbContext<AuthorDbContext>(opt => opt.UseInMemoryDatabase("AuthorsDb"));
            }
        }

        public static void ApplyDbMigration(this WebApplication app)
        {
            if (app.Environment.IsProduction())
            {
                using var scope = app.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AuthorDbContext>();
                
                try
                {
                    context.Database.Migrate();
                }
                catch (SqlException ex)
                {
                    var logger = app.Services.GetService<ILogger<AuthorDbContext>>();

                    if (logger == null)
                        throw;

                    logger.LogError($"Migration error: {ex.Message}");
                }
            }
        }

        public static void AddAuthorsMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(opt =>
            {
                opt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.AddRabbitMqSharedHost(configuration);
                });
            });
        }
    }
}
