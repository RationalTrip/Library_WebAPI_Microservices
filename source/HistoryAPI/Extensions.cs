using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using HistoryAPI.Repository;
using HistoryAPI.DataTransit.BookCreateConsumer;
using HistoryAPI.DataTransit.VisitorCreateConsumer;
using MassTransit;
using LibraryTransit.Contract.Configuration;

namespace HistoryAPI
{
    static class Extensions
    {
        public static void AddHistoryDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                string connectionString = configuration.GetConnectionString("HistoryDb");
                services.AddDbContext<HistoryDbContext>(opt => opt.UseSqlServer(connectionString));
            }
            else
            {
                services.AddDbContext<HistoryDbContext>(opt => opt.UseInMemoryDatabase("HistoryDb"));
            }
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IVisitorRepository, VisitorRepository>();
            services.AddScoped<IHistoryRepository, HistoryRepository>();
        }

        public static void ApplyDbMigration(this WebApplication app)
        {
            if (app.Environment.IsProduction())
            {
                using var scope = app.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<HistoryDbContext>();
                
                try
                {
                    context.Database.Migrate();
                }
                catch (SqlException ex)
                {
                    var logger = app.Services.GetService<ILogger<HistoryDbContext>>();

                    if (logger == null)
                        throw;

                    logger.LogError($"Migration error: {ex.Message}");
                }
            }
        }

        public static void AddHistoryMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<BookConsumer>();
            services.AddTransient<BookConsumerDefinition>();

            services.AddTransient<VisitorConsumer>();
            services.AddTransient<VisitorConsumerDefinition>();

            services.AddMassTransit(opt =>
            {
                opt.AddConsumer<BookConsumer>(typeof(BookConsumerDefinition));

                opt.AddConsumer<VisitorConsumer>(typeof(VisitorConsumerDefinition));

                opt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.AddRabbitMqSharedHost(configuration);

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
