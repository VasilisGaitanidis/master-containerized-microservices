﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.UseCases.DeleteCatalogItem
{
    public class DeleteCatalogItemCommandHandler : IRequestHandler<DeleteCatalogItemCommand, bool>
    {
        private readonly ICatalogItemRepository _catalogItemRepository;

        public DeleteCatalogItemCommandHandler(ICatalogItemRepository catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
        }

        public async Task<bool> Handle(DeleteCatalogItemCommand request, CancellationToken cancellationToken)
        {
            var catalogItem = await _catalogItemRepository.GetCatalogItemAsync(request.Id);

            if (catalogItem == null)
                return false;

            _catalogItemRepository.Delete(catalogItem);

            return true;
        }
    }
}