using FluentValidation;

namespace Catalog.Application.UseCases.UpdateCatalogItem
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