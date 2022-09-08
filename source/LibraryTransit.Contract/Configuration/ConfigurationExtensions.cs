using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace LibraryTransit.Contract.Configuration
{
    public static class ConfigurationExtensions
    {
        public static void AddLibraryTransitSharedConfiguration(this IConfigurationBuilder configBuilder, IWebHostEnvironment env)
        {
            configBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location));

            if (env.IsProduction())
            {
                configBuilder.AddJsonFile("libraryTransitSharedConfig.Production.json");
            }
            else
            {
                configBuilder.AddJsonFile("libraryTransitSharedConfig.Development.json");
            }

            configBuilder.SetBasePath(env.ContentRootPath);
        }

        public static void AddRabbitMqSharedHost(this IRabbitMqBusFactoryConfigurator hostBuilder, IConfiguration configuration)
        {
            var hostSection = configuration.GetRequiredSection("LibraryTransit:connection");

            hostBuilder.Host(hostSection["host"], ushort.Parse(hostSection["port"]), hostSection["virtualHost"], host =>
            {
                host.Username(hostSection["login"]);
                host.Password(hostSection["password"]);
            });
        }
    }
}
