using BookAPI.DataTransit.AuthorCreateConsumer;
using BookAPI.Repository;
using LibraryTransit.Contract.Configuration;
using MassTransit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookAPI
{
    static class Extensions
    {
        public static void AddBookDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                string connectionString = configuration.GetConnectionString("BooksDb");
                services.AddDbContext<BookDbContext>(opt => opt.UseSqlServer(connectionString));
            }
            else
            {
                services.AddDbContext<BookDbContext>(opt => opt.UseInMemoryDatabase("BooksDb"));
            }
        }

        public static void ApplyDbMigration(this WebApplication app)
        {
            if (app.Environment.IsProduction())
            {
                using var scope = app.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<BookDbContext>();
                
                try
                {
                    context.Database.Migrate();
                }
                catch (SqlException ex)
                {
                    var logger = app.Services.GetService<ILogger<BookDbContext>>();

                    if (logger == null)
                        throw;

                    logger.LogError($"Migration error: {ex.Message}");
                }
            }
        }

        public static void AddBookMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<AuthorConsumer>();
            services.AddTransient<AuthorConsumerDefinition>();

            services.AddMassTransit(opt =>
            {
                opt.AddConsumer<AuthorConsumer>(typeof(AuthorConsumerDefinition));

                opt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.AddRabbitMqSharedHost(configuration);

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
