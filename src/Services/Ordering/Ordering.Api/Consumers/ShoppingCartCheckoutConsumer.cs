using System;
using System.Threading.Tasks;
using Contracts.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Ordering.Api.Consumers
{
    public class ShoppingCartCheckoutConsumer : IConsumer<ShoppingCartCheckoutIntegrationEvent>
    {
        private readonly ILogger<ShoppingCartCheckoutConsumer> _logger;

        public ShoppingCartCheckoutConsumer(ILogger<ShoppingCartCheckoutConsumer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<ShoppingCartCheckoutIntegrationEvent> context)
        {
            _logger.LogInformation($"{nameof(ShoppingCartCheckoutConsumer)} - happened with correlation Id '{0}'", context.CorrelationId);
            _logger.LogInformation($"{nameof(ShoppingCartCheckoutConsumer)} - with message '{0}' ", context.Message.ToString());

            await Task.Delay(300);

            await Task.FromResult(1);
        }
    }
}