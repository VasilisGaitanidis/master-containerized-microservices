﻿using System;
using Catalog.Application.Dtos.Responses;
using MediatR;

namespace Catalog.Application.UseCases.Commands.CreateCatalogItem
{
    public class CreateCatalogItemCommand : IRequest<CatalogItemDto>
    {
        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public int Stock { get; }

        public Guid CatalogTypeId { get; }

        public CreateCatalogItemCommand(string name, string description, decimal price, int stock, Guid catalogTypeId)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CatalogTypeId = catalogTypeId;
        }
    }
}