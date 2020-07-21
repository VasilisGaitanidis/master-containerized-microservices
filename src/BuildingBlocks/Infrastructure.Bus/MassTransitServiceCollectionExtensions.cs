using System.Threading.Tasks;
using Common;
using Infrastructure.Bus.Middlewares;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Bus
{
    public static class MassTransitServiceCollectionExtensions
    {
        public static IServiceCollection AddProducer(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqOptions = configuration.GetOptions<RabbitMqOptions>("rabbitMq");

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(rabbitMqOptions.Host, rabbitMqOptions.VirtualHost, hostConfigurator =>
                    {
                        hostConfigurator.Username(rabbitMqOptions.Username);
                        hostConfigurator.Password(rabbitMqOptions.Password);
                    });
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }

        public static IServiceCollection AddConsumer(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqOptions = configuration.GetOptions<RabbitMqOptions>("rabbitMq");

            services.AddMassTransit(x =>
            {
                x.AddConsumer<EventConsumer>();

                x.UsingRabbitMq((context, configurator) =>
                {
                    // configurator.SendTopology.UseCorrelationId<IMessage>(message => message.CorrelationId);

                    configurator.Host(rabbitMqOptions.Host, rabbitMqOptions.VirtualHost, hostConfigurator =>
                    {
                        hostConfigurator.Username(rabbitMqOptions.Username);
                        hostConfigurator.Password(rabbitMqOptions.Password);
                    });

                    configurator.ReceiveEndpoint("event-listener", e =>
                    {
                        e.ConfigureConsumer<EventConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqOptions = configuration.GetOptions<RabbitMqOptions>("rabbitMq");

            services.AddMassTransit(configurator =>
            {
                MassTransit.Bus.Factory.CreateUsingRabbitMq(factoryConfigurator =>
                {
                    factoryConfigurator.Host(rabbitMqOptions.Host, rabbitMqOptions.VirtualHost, hostConfigurator =>
                    {
                        hostConfigurator.Username(rabbitMqOptions.Username);
                        hostConfigurator.Password(rabbitMqOptions.Password);
                    });
                });
            });

            return services;
        }
    }

    internal interface IValueEntered
    {
        string Value { get; }
    }

    class EventConsumer : IConsumer<IValueEntered>
    {
        readonly ILogger<EventConsumer> _logger;

        public EventConsumer(ILogger<EventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IValueEntered> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message.Value);
        }
    }
}