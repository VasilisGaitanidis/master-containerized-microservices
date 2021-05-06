using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.IntegrationEvents;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Dtos.Requests;
using Ordering.Application.UseCases.Commands.CreateOrder;

namespace Ordering.Api.Consumers
{
    public class ShoppingCartCheckoutConsumer : IConsumer<ShoppingCartCheckoutIntegrationEvent>
    {
        private readonly ILogger<ShoppingCartCheckoutConsumer> _logger;
        private readonly IMediator _mediator;

        public ShoppingCartCheckoutConsumer(ILogger<ShoppingCartCheckoutConsumer> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Consume(ConsumeContext<ShoppingCartCheckoutIntegrationEvent> context)
        {
            _logger.LogInformation($"{nameof(ShoppingCartCheckoutConsumer)} - happened with correlation Id '{0}'", context.CorrelationId);
            _logger.LogInformation($"{nameof(ShoppingCartCheckoutConsumer)} - with message '{0}' ", context.Message.ToString());

            IList<ShoppingCartItemDto> shoppingCartItems = context.Message.Items.Select(shoppingCartItem =>
                    new ShoppingCartItemDto
                    {
                        Quantity = shoppingCartItem.Quantity,
                        Color = shoppingCartItem.Color,
                        Price = shoppingCartItem.Price,
                        ProductName = shoppingCartItem.ProductName
                    })
                .ToList();

            var createOrderCommand = new CreateOrderCommand(context.Message.Username,
                context.Message.TotalPrice,
                context.Message.ShippingAddress,
                new BuyerDto
                {
                    Country = context.Message.Buyer.Country,
                    Email = context.Message.Buyer.Email,
                    FirstName = context.Message.Buyer.FirstName,
                    LastName = context.Message.Buyer.LastName,
                    State = context.Message.Buyer.State,
                    ZipCode = context.Message.Buyer.ZipCode
                },
                shoppingCartItems);

            await _mediator.Send(createOrderCommand);
        }
    }
}