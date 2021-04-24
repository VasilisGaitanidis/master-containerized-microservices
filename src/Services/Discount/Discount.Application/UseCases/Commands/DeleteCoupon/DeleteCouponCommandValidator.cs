using FluentValidation;

namespace Discount.Application.UseCases.Commands.DeleteCoupon
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