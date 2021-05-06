using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ordering.Domain.Repositories;

namespace Ordering.Application.UseCases.Commands.CreateOrder
{
    /// <summary>
    /// The create order command handler.
    /// </summary>
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrderCommandHandler"/>.
        /// </summary>
        /// <param name="orderRepository">The order repository.</param>
        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        /// <inheritdoc />
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // TODO save order, buyer and order items
            await Task.Delay(300, cancellationToken);

            return Guid.NewGuid();
        }
    }
}