using FluentValidation;

namespace Discount.Application.UseCases.Commands.CreateCoupon
{
    public class CreateCouponCommandValidator : AbstractValidator<CreateCouponCommand>
    {
        public CreateCouponCommandValidator()
        {
            RuleFor(c => c.ProductName)
                .NotEmpty();

            RuleFor(c => c.Amount)
                .GreaterThan(0);
        }
    }
}