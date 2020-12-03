using System.Reflection;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Repositories;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddRepositories()
                .AddCustomDbContext(configuration)
                .AddInfrastructure(configuration);
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<ICatalogItemRepository, CatalogItemRepository>();
        }

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