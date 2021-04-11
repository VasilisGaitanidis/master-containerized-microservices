using System.Threading;
using System.Threading.Tasks;
using Discount.Application.Dtos.Responses;
using MediatR;

namespace Discount.Application.UseCases.Commands.CreateCoupon
{
    public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, CouponDto>
    {
        public Task<CouponDto> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}