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

        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqOptions = configuration.GetOptions<RabbitMqOptions>("RabbitMq");

            return services.AddMassTransit(x =>
            {
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
    }
}