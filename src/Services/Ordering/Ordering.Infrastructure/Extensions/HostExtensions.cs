using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase(this IHost host) =>
            host.MigrateEfCoreDatabase<OrderingDataContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<OrderingDataContextSeed>>();

                OrderingDataContextSeed
                    .SeedAsync(context, logger)
                    .Wait();
            });
    }
}
