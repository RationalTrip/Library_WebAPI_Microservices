using LibraryTransit.Contract.Configuration;
using MassTransit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VisitorAPI.Repository;

namespace BookAPI
{
    static class Extensions
    {
        public static void AddVisitorDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                string connectionString = configuration.GetConnectionString("VisitorsDb");
                services.AddDbContext<VisitorDbContext>(opt => opt.UseSqlServer(connectionString));
            }
            else
            {
                services.AddDbContext<VisitorDbContext>(opt => opt.UseInMemoryDatabase("VisitorsDb"));
            }
        }

        public static void ApplyDbMigration(this WebApplication app)
        {
            if (app.Environment.IsProduction())
            {
                using var scope = app.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<VisitorDbContext>();
                
                try
                {
                    context.Database.Migrate();
                }
                catch (SqlException ex)
                {
                    var logger = app.Services.GetService<ILogger<VisitorDbContext>>();

                    if (logger == null)
                        throw;

                    logger.LogError($"Migration error: {ex.Message}");
                }
            }
        }

        public static void AddVisitorMassTransit(this IServiceCollection services, IConfiguration configuration)
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
