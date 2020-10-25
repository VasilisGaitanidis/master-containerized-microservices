using Catalog.Application.Commands;
using FluentValidation;

namespace Catalog.Application.Validations
{
    public class UpdateCatalogItemCommandValidator : AbstractValidator<UpdateCatalogItemCommand>
    {
        public UpdateCatalogItemCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty();

            RuleFor(c => c.Price)
                .GreaterThan(0);

            RuleFor(c => c.Stock)
                .GreaterThan(0);
        }
    }
}