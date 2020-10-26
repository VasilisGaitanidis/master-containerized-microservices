using FluentValidation;

namespace Catalog.Application.UseCases.CreateCatalogItem
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