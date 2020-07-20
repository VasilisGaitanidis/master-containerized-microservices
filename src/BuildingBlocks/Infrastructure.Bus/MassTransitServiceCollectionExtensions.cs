using System;
using Common;
using Infrastructure.Bus.Middlewares;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Bus
{
    public static class MassTransitServiceCollectionExtensions
    {
        public static IServiceCollection AddMassTransit1(this IServiceCollection services, IConfiguration configuration)
        {

            throw new NotImplementedException();
            //services.AddMassTransit(configurator =>
            //{
            //    var rabbitMqOptions = configuration.GetOptions<RabbitMqOptions>("rabbitMq");

            //    return Bus.Factory.CreateUsingRabbitMq(cfg =>
            //    {
            //        var host = cfg.Host(new Uri(rabbitMqOptions.Url), "/", hc =>
            //        {
            //            hc.Username(rabbitMqOptions.UserName);
            //            hc.Password(rabbitMqOptions.Password);
            //        });

            //        cfg.ReceiveEndpoint("contact", x =>
            //        {
            //            x.ConfigureConsumer<ContactCreatedConsumer>(provider);
            //        });

            //        cfg.PropagateOpenTracingContext();
            //        cfg.PropagateCorrelationIdContext();
            //    });
            //}, (cfg) =>
            //{
            //    cfg.AddConsumersFromNamespaceContaining<ConsumerAnchor>();
            //});

            //return services;

        }
    }
}