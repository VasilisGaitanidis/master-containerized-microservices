using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Api.Application.Commands;
using Catalog.Api.Application.Dtos;
using Catalog.Domain.Models;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Api.Application.Handlers
{
    public class CreateCatalogItemHandler : IRequestHandler<CreateCatalogItemCommand, CatalogItemResponseDto>
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly IMapper _mapper;

        public CreateCatalogItemHandler(ICatalogItemRepository catalogItemRepository, IMapper mapper)
        {
            _catalogItemRepository = catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CatalogItemResponseDto> Handle(CreateCatalogItemCommand request, CancellationToken cancellationToken)
        {
            var catalogItem = new CatalogItem(request.Name, request.Description, request.Price, request.Stock, request.CatalogTypeId);

            await _catalogItemRepository.AddAsync(catalogItem);

            await _catalogItemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CatalogItemResponseDto>(catalogItem);
        }
    }
}