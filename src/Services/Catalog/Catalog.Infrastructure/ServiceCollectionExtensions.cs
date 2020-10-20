using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Repositories;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services)
        {
            return services
                .AddRepositories()
                .AddInfrastructure();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<ICatalogItemRepository, CatalogItemRepository>();
        }
    }
}