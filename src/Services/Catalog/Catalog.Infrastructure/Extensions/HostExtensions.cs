using Catalog.Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase(this IHost host) =>
            host.MigrateEfCoreDatabase<CatalogDataContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<CatalogDataContextSeed>>();

                CatalogDataContextSeed
                    .SeedAsync(context, logger)
                    .Wait();
            });
    }
}
