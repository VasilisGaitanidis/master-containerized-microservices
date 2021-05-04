using System.Reflection;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure.Extensions
{
    /// <summary>
    /// The ordering infrastructure <see cref="IServiceCollection"/> extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add ordering infrastructure on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddOrderingInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddRepositories()
                .AddCustomDbContext(configuration)
                .AddEntityFrameworkUnitOfWork()
                .AddDomainEventDispatcher()
                .AddConsulServiceDiscovery(configuration);
        }

        /// <summary>
        /// Add ordering repositories on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IOrderRepository, OrderRepository>();
        }

        /// <summary>
        /// Add ordering database context on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OrderingSqlServer");

            services.AddDbContext<OrderingDataContext>(options =>
            {
                options.UseLazyLoadingProxies().
                    UseSqlServer(connectionString,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                        }
                        );
            });

            services.AddScoped<AppDbContext>(provider => provider.GetService<OrderingDataContext>());

            return services;
        }
    }
}