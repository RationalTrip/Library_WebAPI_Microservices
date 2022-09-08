using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using BorrowedAPI.Repository;
using BorrowedAPI.DataTransit.BookCreateConsumer;
using MassTransit;
using LibraryTransit.Contract.Configuration;
using BorrowedAPI.DataTransit.VisitorCreateConsumer;

namespace BorrowedAPI
{
    static class Extensions
    {
        public static void AddBorrowedDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                string connectionString = configuration.GetConnectionString("BorrowedDb");
                services.AddDbContext<BorrowedDbContext>(opt => opt.UseSqlServer(connectionString));
            }
            else
            {
                services.AddDbContext<BorrowedDbContext>(opt => opt.UseInMemoryDatabase("BorrowedDb"));
            }
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IVisitorRepository, VisitorRepository>();
            services.AddScoped<IBorrowedRepository, BorrowedRepository>();
        }

        public static void ApplyDbMigration(this WebApplication app)
        {
            if (app.Environment.IsProduction())
            {
                using var scope = app.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<BorrowedDbContext>();
                
                try
                {
                    context.Database.Migrate();
                }
                catch (SqlException ex)
                {
                    var logger = app.Services.GetService<ILogger<BorrowedDbContext>>();

                    if (logger == null)
                        throw;

                    logger.LogError($"Migration error: {ex.Message}");
                }
            }
        }

        public static void AddBorrowedMassTransit(this IServiceCollection services, IConfiguration configuration)
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
