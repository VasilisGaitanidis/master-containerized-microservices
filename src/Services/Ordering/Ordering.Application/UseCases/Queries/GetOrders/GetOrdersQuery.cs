using System.Collections.Generic;
using MediatR;
using Ordering.Application.Dtos.Responses;

namespace Ordering.Application.UseCases.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
    }
}