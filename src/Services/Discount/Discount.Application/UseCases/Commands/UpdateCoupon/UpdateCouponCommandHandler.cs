using System;
using System.Threading;
using System.Threading.Tasks;
using Discount.Application.Exceptions;
using Discount.Domain.Repositories;
using MediatR;

namespace Discount.Application.UseCases.Commands.UpdateCoupon
{
    /// <summary>
    /// The update coupon command handler.
    /// </summary>
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, Unit>
    {
        private readonly ICouponRepository _couponRepository;

        /// <summary>
        /// Initializes a new instance of a <see cref="UpdateCouponCommandHandler"/>.
        /// </summary>
        /// <param name="couponRepository"></param>
        public UpdateCouponCommandHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByProductNameAsync(request.ProductName) ??
                throw new CouponNotFoundException(request.ProductName);

            coupon.ChangeDescription(request.Description);
            coupon.ChangeAmount(request.Amount);

            _couponRepository.Update(coupon);

            return Unit.Value;
        }
    }
}