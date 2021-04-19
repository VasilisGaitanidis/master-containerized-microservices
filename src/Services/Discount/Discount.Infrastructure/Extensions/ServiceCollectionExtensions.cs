using System.Reflection;
using Discount.Domain.Repositories;
using Discount.Infrastructure.Data;
using Discount.Infrastructure.Repositories;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Infrastructure.Extensions
{
    /// <summary>
    /// The discount infrastructure <see cref="IServiceCollection"/> extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add discount infrastructure on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddDiscountInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddRepositories()
                .AddCustomDbContext(configuration)
                .AddEntityFrameworkUnitOfWork()
                .AddDomainEventDispatcher();
        }

        /// <summary>
        /// Add discount repositories on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<ICouponRepository, CouponRepository>();
        }

        /// <summary>
        /// Add discount database context on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DiscountSqlServer");

            services.AddDbContext<DiscountDataContext>(options =>
            {
                options.UseLazyLoadingProxies().
                    UseSqlServer(connectionString,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                        }
                    );
            });

            services.AddScoped<AppDbContext>(provider => provider.GetService<DiscountDataContext>());

            return services;
        }
    }
}