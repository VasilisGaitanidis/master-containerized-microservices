using FluentValidation;

namespace Ordering.Application.UseCases.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(c => c.Username)
                .NotEmpty();

            RuleFor(c => c.ShippingAddress)
                .NotEmpty();

            RuleFor(c => c.TotalPrice)
                .GreaterThanOrEqualTo(0);

            RuleFor(c => c.Buyer)
                .NotNull();

            RuleFor(c => c.Buyer.FirstName)
                .NotEmpty();

            RuleFor(c => c.Buyer.LastName)
                .NotEmpty();

            RuleFor(c => c.Buyer.Country)
                .NotEmpty();

            RuleFor(c => c.Buyer.State)
                .NotEmpty();

            RuleFor(c => c.Buyer.ZipCode)
                .NotEmpty();
        }
    }
}