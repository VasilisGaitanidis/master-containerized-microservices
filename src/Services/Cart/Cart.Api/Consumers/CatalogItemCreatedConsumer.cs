using System;
using System.Threading.Tasks;
using Contracts.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Cart.Api.Consumers
{
    public class CatalogItemCreatedConsumer : IConsumer<CatalogItemCreatedIntegrationEvent>
    {
        private readonly ILogger<CatalogItemCreatedConsumer> _logger;

        public CatalogItemCreatedConsumer(ILogger<CatalogItemCreatedConsumer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<CatalogItemCreatedIntegrationEvent> context)
        {
            _logger.LogInformation($"CatalogItemCreatedConsumer - happened with correlation Id {context.CorrelationId}");
            _logger.LogInformation("CatalogItemCreatedConsumer - with message '{0}' ", context.Message.ToString());

            await Task.Delay(300);

            await Task.FromResult(1);
        }
    }
}