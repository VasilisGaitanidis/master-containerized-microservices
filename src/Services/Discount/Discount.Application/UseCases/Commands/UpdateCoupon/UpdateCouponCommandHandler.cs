using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Discount.Application.UseCases.Commands.UpdateCoupon
{
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, Unit>
    {
        public Task<Unit> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}