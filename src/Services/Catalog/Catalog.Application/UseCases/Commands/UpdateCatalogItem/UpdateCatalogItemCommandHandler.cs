using System;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Exceptions;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.UseCases.Commands.UpdateCatalogItem
{
    public class UpdateCatalogItemCommandHandler : IRequestHandler<UpdateCatalogItemCommand, Unit>
    {
        private readonly ICatalogItemRepository _catalogItemRepository;

        public UpdateCatalogItemCommandHandler(ICatalogItemRepository catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
        }

        public async Task<Unit> Handle(UpdateCatalogItemCommand request, CancellationToken cancellationToken)
        {
            var catalogItem = await _catalogItemRepository.GetCatalogItemAsync(request.Id);

            if (catalogItem == null)
                throw new CatalogItemNotFoundException(request.Id);

            catalogItem.ChangeName(request.Name);
            catalogItem.ChangeDescription(request.Description);
            catalogItem.ChangePrice(request.Price);
            catalogItem.ChangeStock(request.Stock);
            catalogItem.ChangeCatalogTypeId(request.CatalogTypeId);

            _catalogItemRepository.Update(catalogItem);

            return Unit.Value;
        }
    }
}