using Cart.Api.Configuration;
using Cart.Api.Consumers;
using Cart.Api.Filters;
using Infrastructure.Extensions;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Cart.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
            });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerOptions = configuration.GetOptions<SwaggerOptions>("Swagger");

            return services
                .AddSwaggerGen(x =>
                {
                    x.SwaggerDoc(swaggerOptions.Name, new OpenApiInfo
                    {
                        Title = swaggerOptions.Title,
                        Version = swaggerOptions.Version,
                        Description = swaggerOptions.Description
                    });
                });
        }

        public static IServiceCollection AddCustomMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqOptions = configuration.GetOptions<RabbitMqOptions>("RabbitMq");

            return services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.AddConsumer<CatalogItemCreatedConsumer>();

                x.UsingRabbitMq((context, configurator) =>
                {
                    configurator.UseHealthCheck(context);
                    configurator.Host(rabbitMqOptions.Host, rabbitMqOptions.VirtualHost, h =>
                    {
                        h.Username(rabbitMqOptions.Username);
                        h.Password(rabbitMqOptions.Password);
                    });
                    configurator.ConfigureEndpoints(context);
                });
            }).AddMassTransitHostedService();
        }

        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var healthChecksBuilder = services.AddHealthChecks();

            healthChecksBuilder.AddRedis(
                configuration.GetConnectionString("Redis"),
                name: "Redis-check",
                tags: new[] { "cartredis" });

            return services;
        }
    }
}