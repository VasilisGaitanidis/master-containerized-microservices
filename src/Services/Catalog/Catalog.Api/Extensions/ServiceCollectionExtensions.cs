using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Catalog.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services
                .AddSwaggerGen(x =>
                {
                    x.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Catalog API",
                        Version = "v1",
                        Description = "The catalog microservice."
                    });
                });
        }
    }
}