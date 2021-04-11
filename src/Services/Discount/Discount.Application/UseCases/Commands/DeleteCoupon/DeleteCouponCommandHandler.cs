using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Discount.Application.UseCases.Commands.DeleteCoupon
{
    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, Unit>
    {
        public Task<Unit> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}