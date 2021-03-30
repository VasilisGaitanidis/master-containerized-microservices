using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cart.Application.Dtos.Responses;
using Cart.Domain.Repositories;
using MediatR;

namespace Cart.Application.UseCases.Queries.GetShoppingCartByUsername
{
    public class GetShoppingCartByUsernameQueryQueryHandler : IRequestHandler<GetShoppingCartByUsernameQuery, ShoppingCartDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public GetShoppingCartByUsernameQueryQueryHandler(ICartRepository catalogItemRepository, IMapper mapper)
        {
            _cartRepository = catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ShoppingCartDto> Handle(GetShoppingCartByUsernameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _cartRepository.GetShoppingCartAsync(request.Username);

            return _mapper.Map<ShoppingCartDto>(shoppingCart);
        }
    }
}