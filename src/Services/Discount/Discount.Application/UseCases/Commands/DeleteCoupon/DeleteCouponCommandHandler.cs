using System;
using System.Threading;
using System.Threading.Tasks;
using Discount.Application.Exceptions;
using Discount.Domain.Repositories;
using MediatR;

namespace Discount.Application.UseCases.Commands.DeleteCoupon
{
    /// <summary>
    /// The delete coupon command handler.
    /// </summary>
    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, Unit>
    {
        private readonly ICouponRepository _couponRepository;

        /// <summary>
        /// Initializes a new instance of a <see cref="DeleteCouponCommandHandler"/>.
        /// </summary>
        /// <param name="couponRepository"></param>
        public DeleteCouponCommandHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByProductNameAsync(request.ProductName) ??
                throw new CouponNotFoundException(request.ProductName);

            _couponRepository.Delete(coupon);

            return Unit.Value;
        }
    }
}