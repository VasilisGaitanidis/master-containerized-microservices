using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Dtos;
using Catalog.Application.Queries;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetCatalogItemsHandler : IRequestHandler<GetCatalogItemsQuery, IEnumerable<CatalogItemResponseDto>>
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly IMapper _mapper;

        public GetCatalogItemsHandler(ICatalogItemRepository catalogItemRepository, IMapper mapper)
        {
            _catalogItemRepository = catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CatalogItemResponseDto>> Handle(GetCatalogItemsQuery request, CancellationToken cancellationToken)
        {
            var catalogItems = await _catalogItemRepository.GetCatalogItemsAsync();

            return _mapper.Map<List<CatalogItemResponseDto>>(catalogItems);
        }
    }
}