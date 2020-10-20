using System;

namespace Catalog.Application.Dtos.Requests
{
    public class UpdateCatalogItemDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public Guid CatalogTypeId { get; set; }
    }
}