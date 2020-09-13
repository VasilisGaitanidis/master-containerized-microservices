using System;
using Catalog.Api.Application.Dtos;
using MediatR;

namespace Catalog.Api.Application.Commands
{
    public class CreateCatalogItemCommand : IRequest<CatalogItemResponseDto>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public Guid CatalogTypeId { get; set; }
    }
}