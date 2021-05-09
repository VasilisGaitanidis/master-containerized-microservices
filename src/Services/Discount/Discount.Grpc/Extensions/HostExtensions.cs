using Discount.Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Discount.Grpc.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase(this IHost host) =>
            host.MigrateEfCoreDatabase<DiscountDataContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<DiscountDataContextSeed>>();

                DiscountDataContextSeed
                    .SeedAsync(context, logger)
                    .Wait();
            });
    }
}
