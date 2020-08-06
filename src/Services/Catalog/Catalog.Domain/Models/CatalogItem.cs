using System;
using Domain.Core.Models;

namespace Catalog.Domain.Models
{
    public class CatalogItem : AggregateRoot<Guid>
    {
        private string _name;

        private string _description;

        private decimal _price;

        private int _stock;

        private CatalogType _catalogType;

        public CatalogItem(string name, string description, decimal price, int stock, CatalogType catalogType)
            : base(Guid.NewGuid())
        {
            _name = name;
            _description = description;
            _price = price;
            _stock = stock;
            _catalogType = catalogType;
        }

    }
}