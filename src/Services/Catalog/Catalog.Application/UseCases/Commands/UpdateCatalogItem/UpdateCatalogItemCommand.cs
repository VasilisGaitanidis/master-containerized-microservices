﻿using System;
using MediatR;

namespace Catalog.Application.UseCases.Commands.UpdateCatalogItem
{
    public class UpdateCatalogItemCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public int Stock { get; }

        public Guid CatalogTypeId { get; }

        public UpdateCatalogItemCommand(Guid id, string name, string description, decimal price, int stock, Guid catalogTypeId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CatalogTypeId = catalogTypeId;
        }
    }
}