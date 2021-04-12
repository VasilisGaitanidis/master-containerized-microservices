using Discount.Application.UseCases.Commands.DeleteCoupon;
using FluentValidation;

namespace Discount.Application.UseCases.Commands.UpdateCoupon
{
    /// <summary>
    /// The delete coupon command validator.
    /// </summary>
    public class DeleteCouponCommandValidator : AbstractValidator<DeleteCouponCommand>
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="DeleteCouponCommandValidator"/>.
        /// </summary>
        public DeleteCouponCommandValidator()
        {
            RuleFor(c => c.ProductName)
                .NotEmpty();
        }
    }
}