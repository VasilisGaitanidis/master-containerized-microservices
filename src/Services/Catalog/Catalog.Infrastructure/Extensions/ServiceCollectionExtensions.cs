using System.Reflection;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.Extensions
{
    /// <summary>
    /// The catalog infrastructure <see cref="IServiceCollection"/> extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add catalog infrastructure on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddRepositories()
                .AddCustomDbContext(configuration)
                .AddEntityFrameworkUnitOfWork()
                .AddDomainEventDispatcher()
                .AddConsulServiceDiscovery(configuration);
        }

        /// <summary>
        /// Add catalog repositories on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<ICatalogItemRepository, CatalogItemRepository>();
        }

        /// <summary>
        /// Add catalog database context on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CatalogSqlServer");

            services.AddDbContext<CatalogDataContext>(options =>
            {
                options.UseLazyLoadingProxies().
                    UseSqlServer(connectionString,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                        }
                        );
            });

            services.AddScoped<AppDbContext>(provider => provider.GetService<CatalogDataContext>());

            return services;
        }
    }
}