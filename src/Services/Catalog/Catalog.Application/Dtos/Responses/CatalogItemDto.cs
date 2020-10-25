using System;

namespace Catalog.Application.Dtos.Responses
{
    public class CatalogItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public CatalogTypeDto CatalogType { get; set; }
    }
}