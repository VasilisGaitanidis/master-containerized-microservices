using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cart.Application.Dtos.Responses;
using Cart.Application.Exceptions;
using Cart.Domain.Entities;
using Cart.Domain.Repositories;
using Cart.Infrastructure.Services.Grpc;
using MediatR;

namespace Cart.Application.UseCases.Commands.UpdateShoppingCart
{
    /// <summary>
    /// The update shopping cart command.
    /// </summary>
    public class UpdateShoppingCartCommandHandler : IRequestHandler<UpdateShoppingCartCommand, ShoppingCartDto>
    {
        private readonly IDiscountGrpcService _discountGrpcService;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="UpdateShoppingCartCommandHandler"/>.
        /// </summary>
        /// <param name="discountGrpcService">The discount gRPC service.</param>
        /// <param name="cartRepository">The cart repository.</param>
        /// <param name="mapper">The mapper.</param>
        public UpdateShoppingCartCommandHandler(IDiscountGrpcService discountGrpcService, ICartRepository cartRepository, IMapper mapper)
        {
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc />
        public async Task<ShoppingCartDto> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = new ShoppingCart(request.Username);

            foreach (var item in request.Items)
            {
                var couponResponse = await _discountGrpcService.GetCouponAsync(item.ProductName);
                var finalPrice = item.Price - couponResponse.Amount;

                if (finalPrice < 0)
                {
                    throw new ShoppingCartItemNegativePriceException(item.ProductName);
                }

                shoppingCart.AddShoppingCartItems(item.Quantity, item.Color, finalPrice, item.ProductName);
            }

            var updatedShoppingCart = await _cartRepository.UpdateShoppingCartAsync(shoppingCart);

            return _mapper.Map<ShoppingCartDto>(updatedShoppingCart);
        }
    }
}