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
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiscountInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddRepositories()
                .AddCustomDbContext(configuration)
                .AddInfrastructure(configuration);
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<ICouponRepository, CouponRepository>();
        }

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