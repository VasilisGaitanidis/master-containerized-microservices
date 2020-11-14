using Catalog.Api.Configuration;
using Catalog.Api.Filters;
using Infrastructure;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Catalog.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers(
                options =>
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

                x.UsingRabbitMq((context, configurator) =>
                {
                    configurator.UseHealthCheck(context);
                    configurator.Host(rabbitMqOptions.Host, rabbitMqOptions.VirtualHost, h =>
                    {
                        h.Username(rabbitMqOptions.Username);
                        h.Password(rabbitMqOptions.Password);
                    });
                });
            }).AddMassTransitHostedService();
        }

        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var healthChecksBuilder = services.AddHealthChecks();

            healthChecksBuilder.AddSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                name: "CatalogSqlServer-check",
                tags: new[] { "catalogsqlserver" });

            var rabbitMqOptions = configuration.GetOptions<RabbitMqOptions>("RabbitMq");

            healthChecksBuilder.AddRabbitMQ(
                $"amqp://{rabbitMqOptions.Username}:{rabbitMqOptions.Password}@{rabbitMqOptions.Host}{rabbitMqOptions.VirtualHost}",
                name: "CatalogRabbitMQ-check",
                tags: new[] { "catalograbbitmq" });

            return services;
        }
    }
}