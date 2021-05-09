using System;
using System.Threading;
using System.Threading.Tasks;
using Cart.Domain.Repositories;
using Contracts.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Cart.Application.UseCases.Commands.CheckoutShoppingCart
{
    /// <summary>
    /// The checkout shopping cart command.
    /// </summary>
    public class CheckoutShoppingCartCommandHandler : IRequestHandler<CheckoutShoppingCartCommand, Unit>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        /// <summary>
        /// Initializes a new instance of <see cref="CheckoutShoppingCartCommandHandler"/>.
        /// </summary>
        /// <param name="cartRepository">The cart repository.</param>
        /// <param name="publishEndpoint">The publish endpoint.</param>
        public CheckoutShoppingCartCommandHandler(ICartRepository cartRepository, IPublishEndpoint publishEndpoint)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(CheckoutShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _cartRepository.GetShoppingCartAsync(request.Username);

            await _publishEndpoint.Publish<ShoppingCartCheckoutIntegrationEvent>(new
            {
                request.Username,
                request.TotalPrice,
                request.ShippingAddress,
                request.Buyer,
                shoppingCart?.Items
            }, cancellationToken);

            return Unit.Value;
        }
    }
}