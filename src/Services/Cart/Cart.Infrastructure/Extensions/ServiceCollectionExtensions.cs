using Cart.Domain.Repositories;
using Cart.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cart.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCartInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddRepositories()
                .AddCustomRedis(configuration);
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<ICartRepository, CartRepository>();
        }

        public static IServiceCollection AddCustomRedis(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddStackExchangeRedisCache(options =>
                options.Configuration = configuration.GetConnectionString("Redis"));
        }
    }
}