using System;
using System.Threading;
using System.Threading.Tasks;
using Cart.Domain.Repositories;
using MediatR;

namespace Cart.Application.UseCases.Commands.DeleteShoppingCart
{
    public class DeleteShoppingCartCommandHandler : IRequestHandler<DeleteShoppingCartCommand, Unit>
    {
        private readonly ICartRepository _cartRepository;

        public DeleteShoppingCartCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public async Task<Unit> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
        {
            await _cartRepository.DeleteShoppingCartAsync(request.Username);

            return Unit.Value;
        }
    }
}