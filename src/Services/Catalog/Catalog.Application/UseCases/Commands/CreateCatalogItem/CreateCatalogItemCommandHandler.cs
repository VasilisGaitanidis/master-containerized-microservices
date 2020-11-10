using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Dtos.Responses;
using Catalog.Domain.Models;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.UseCases.Commands.CreateCatalogItem
{
    public class CreateCatalogItemCommandHandler : IRequestHandler<CreateCatalogItemCommand, CatalogItemDto>
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly IMapper _mapper;

        public CreateCatalogItemCommandHandler(ICatalogItemRepository catalogItemRepository, IMapper mapper)
        {
            _catalogItemRepository = catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CatalogItemDto> Handle(CreateCatalogItemCommand request, CancellationToken cancellationToken)
        {
            var catalogItem = new CatalogItem(request.Name, request.Description, request.Price, request.Stock, request.CatalogTypeId);

            await _catalogItemRepository.AddAsync(catalogItem);

            return _mapper.Map<CatalogItemDto>(catalogItem);
        }
    }
}