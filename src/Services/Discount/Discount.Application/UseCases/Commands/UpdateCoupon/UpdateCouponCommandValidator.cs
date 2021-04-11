using FluentValidation;

namespace Discount.Application.UseCases.Commands.UpdateCoupon
{
    public class UpdateCouponCommandValidator : AbstractValidator<UpdateCouponCommand>
    {
        public UpdateCouponCommandValidator()
        {
            RuleFor(c => c.ProductName)
                .NotEmpty();

            RuleFor(c => c.Amount)
                .GreaterThan(0);
        }
    }
}