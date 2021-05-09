using System;
using System.Threading;
using System.Threading.Tasks;
using Discount.Domain.Repositories;
using MediatR;

namespace Discount.Application.UseCases.Commands.DeleteCoupon
{
    /// <summary>
    /// The delete coupon command handler.
    /// </summary>
    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, bool>
    {
        private readonly ICouponRepository _couponRepository;

        /// <summary>
        /// Initializes a new instance of a <see cref="DeleteCouponCommandHandler"/>.
        /// </summary>
        /// <param name="couponRepository">The coupon repository.</param>
        public DeleteCouponCommandHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
        }

        /// <inheritdoc />
        public async Task<bool> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByProductNameAsync(request.ProductName);

            if (coupon == null)
            {
                return false;
            }

            _couponRepository.Delete(coupon);

            return true;
        }
    }
}