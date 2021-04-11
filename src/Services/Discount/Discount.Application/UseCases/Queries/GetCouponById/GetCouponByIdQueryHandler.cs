using System.Threading;
using System.Threading.Tasks;
using Discount.Application.Dtos.Responses;
using MediatR;

namespace Discount.Application.UseCases.Queries.GetCouponById
{
    public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, CouponDto>
    {
        public Task<CouponDto> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}