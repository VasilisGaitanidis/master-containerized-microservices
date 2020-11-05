﻿using System;
using MediatR;

namespace Catalog.Application.UseCases.DeleteCatalogItem
{
    public class DeleteCatalogItemCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public DeleteCatalogItemCommand(Guid id)
        {
            Id = id;
        }
    }
}