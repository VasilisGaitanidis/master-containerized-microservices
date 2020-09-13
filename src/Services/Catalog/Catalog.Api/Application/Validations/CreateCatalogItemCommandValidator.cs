using Catalog.Api.Application.Commands;
using FluentValidation;

namespace Catalog.Api.Application.Validations
{
    public class CreateCatalogItemCommandValidator : AbstractValidator<CreateCatalogItemCommand>
    {
        public CreateCatalogItemCommandValidator()
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