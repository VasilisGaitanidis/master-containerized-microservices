using System;
using Cart.Api.Configuration;
using Cart.Api.Consumers;
using Infrastructure;
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
            services.AddControllers(
                //options =>
                //{
                //    options.Filters.Add<HttpGlobalExceptionFilter>();
                //}
                );

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services
                .AddSwaggerGen(x =>
                {
                    x.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Cart API",
                        Version = "v1",
                        Description = "The cart microservice."
                    });
                });
        }

        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
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
                });
            }).AddMassTransitHostedService();
        }
    }
}