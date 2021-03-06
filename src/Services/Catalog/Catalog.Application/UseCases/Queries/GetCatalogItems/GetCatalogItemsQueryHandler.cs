﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Dtos.Responses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.UseCases.Queries.GetCatalogItems
{
    public class GetCatalogItemsQueryHandler : IRequestHandler<GetCatalogItemsQuery, IEnumerable<CatalogItemDto>>
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly IMapper _mapper;

        public GetCatalogItemsQueryHandler(ICatalogItemRepository catalogItemRepository, IMapper mapper)
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