using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cart.Application.Dtos.Responses;
using Cart.Domain.Entities;
using Cart.Domain.Repositories;
using MediatR;

namespace Cart.Application.UseCases.Commands.UpdateShoppingCart
{
    public class UpdateShoppingCartCommandHandler : IRequestHandler<UpdateShoppingCartCommand, ShoppingCartDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public UpdateShoppingCartCommandHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ShoppingCartDto> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = new ShoppingCart(request.Username);

            foreach (var item in request.Items)
            {
                shoppingCart.AddShoppingCartItems(item.Quantity, item.Color, item.Price, item.ProductName);
            }

            var updatedShoppingCart = await _cartRepository.UpdateShoppingCartAsync(shoppingCart);

            return _mapper.Map<ShoppingCartDto>(updatedShoppingCart);
        }
    }
}