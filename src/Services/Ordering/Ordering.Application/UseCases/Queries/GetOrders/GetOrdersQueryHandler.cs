using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ordering.Application.Dtos.Responses;
using Ordering.Domain.Repositories;

namespace Ordering.Application.UseCases.Queries.GetOrders
{
    /// <summary>
    /// Get orders query handler.
    /// </summary>
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetOrdersQuery"/>.
        /// </summary>
        /// <param name="orderRepository">The order repository.</param>
        /// <param name="mapper">The mapper.</param>
        public GetOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersAsync();

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
    }
}