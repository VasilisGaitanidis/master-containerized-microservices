using System;
using MediatR;

namespace Catalog.Api.Application.Commands
{
    public class UpdateCatalogItemCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public string  Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public Guid CatalogTypeId { get; set; }
    }
}