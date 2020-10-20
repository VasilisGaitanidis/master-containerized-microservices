using System;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Commands;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class UpdateCatalogItemHandler : IRequestHandler<UpdateCatalogItemCommand, bool>
    {
        private readonly ICatalogItemRepository _catalogItemRepository;

        public UpdateCatalogItemHandler(ICatalogItemRepository catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
        }

        public async Task<bool> Handle(UpdateCatalogItemCommand request, CancellationToken cancellationToken)
        {
            var catalogItem = await _catalogItemRepository.GetCatalogItemAsync(request.Id);

            if (catalogItem == null)
                return false;

            catalogItem.ChangeName(request.Name);
            catalogItem.ChangeDescription(request.Description);
            catalogItem.ChangePrice(request.Price);
            catalogItem.ChangeStock(request.Stock);
            catalogItem.ChangeCatalogTypeId(request.CatalogTypeId);

            _catalogItemRepository.Update(catalogItem);

            return true;
        }
    }
}