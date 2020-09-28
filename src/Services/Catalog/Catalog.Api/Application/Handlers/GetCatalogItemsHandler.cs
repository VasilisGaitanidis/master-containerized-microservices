using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Api.Application.Dtos.Responses;
using Catalog.Api.Application.Queries;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Api.Application.Handlers
{
    public class GetCatalogItemsHandler : IRequestHandler<GetCatalogItemsQuery, IEnumerable<CatalogItemDto>>
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly IMapper _mapper;

        public GetCatalogItemsHandler(ICatalogItemRepository catalogItemRepository, IMapper mapper)
        {
            _catalogItemRepository = catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CatalogItemDto>> Handle(GetCatalogItemsQuery request, CancellationToken cancellationToken)
        {
            var catalogItems = await _catalogItemRepository.GetCatalogItemsAsync();

            return _mapper.Map<List<CatalogItemDto>>(catalogItems);
        }
    }
}