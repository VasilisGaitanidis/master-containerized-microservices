using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ordering.Domain.Entities;
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
            Order order = await _orderRepository.AddAsync(CreateOrder(request));

            return order.Id;
        }

        private static Order CreateOrder(CreateOrderCommand request)
        {
            var buyer = new Buyer(request.Buyer.FirstName,
                request.Buyer.LastName,
                request.Buyer.Email,
                request.Buyer.Country,
                request.Buyer.State,
                request.Buyer.ZipCode);

            ICollection<OrderItem> orderItems = request.Items.Select(item => new OrderItem(item.Quantity,
                    item.Price,
                    item.ProductName))
                .ToList();

            var order = new Order(request.Username, request.TotalPrice, request.ShippingAddress, buyer, orderItems);
            return order;
        }
    }
}