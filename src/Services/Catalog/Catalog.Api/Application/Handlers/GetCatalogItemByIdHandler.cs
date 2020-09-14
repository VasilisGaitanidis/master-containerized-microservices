using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Api.Application.Dtos;
using Catalog.Api.Application.Queries;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Api.Application.Handlers
{
    public class GetCatalogItemByIdHandler : IRequestHandler<GetCatalogItemByIdQuery, CatalogItemResponseDto>
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly IMapper _mapper;

        public GetCatalogItemByIdHandler(ICatalogItemRepository catalogItemRepository, IMapper mapper)
        {
            _catalogItemRepository = catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CatalogItemResponseDto> Handle(GetCatalogItemByIdQuery request, CancellationToken cancellationToken)
        {
            var catalogItem = await _catalogItemRepository.GetCatalogItemAsync(request.Id);

            return _mapper.Map<CatalogItemResponseDto>(catalogItem);
        }
    }
}