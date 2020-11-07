using System;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Exceptions;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.UseCases.Commands.DeleteCatalogItem
{
    public class DeleteCatalogItemCommandHandler : IRequestHandler<DeleteCatalogItemCommand, Unit>
    {
        private readonly ICatalogItemRepository _catalogItemRepository;

        public DeleteCatalogItemCommandHandler(ICatalogItemRepository catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
        }

        public async Task<Unit> Handle(DeleteCatalogItemCommand request, CancellationToken cancellationToken)
        {
            var catalogItem = await _catalogItemRepository.GetCatalogItemAsync(request.Id);

            if (catalogItem == null)
                throw new CatalogItemNotFoundException(request.Id);

            _catalogItemRepository.Delete(catalogItem);

            return Unit.Value;
        }
    }
}