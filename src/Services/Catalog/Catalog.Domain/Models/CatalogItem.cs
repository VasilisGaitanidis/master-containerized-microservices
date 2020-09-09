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

        public CatalogType CatalogType { get; private set; }
        private Guid _catalogTypeId;

        /// <summary>
        /// Private constructor without navigational properties (eg. CatalogType)
        /// </summary>
        /// <param name="id">The catalog item identifier.</param>
        /// <param name="name">The catalog item name.</param>
        /// <param name="description">The catalog item description.</param>
        /// <param name="price">The catalog item price.</param>
        /// <param name="stock">The catalog item stock.</param>
        private CatalogItem(Guid id, string name, string description, decimal price, int stock)
            : base(id)
        {
            _name = name;
            _description = description;
            _price = price;
            _stock = stock;
        }

        /// <summary>
        /// Public constructor with navigational properties.
        /// </summary>
        /// <param name="name">The catalog item name.</param>
        /// <param name="description">The catalog item description.</param>
        /// <param name="price">The catalog item price.</param>
        /// <param name="stock">The catalog item stock.</param>
        /// <param name="catalogType">The catalog item type.</param>
        public CatalogItem(string name, string description, decimal price, int stock, CatalogType catalogType)
            : this(Guid.NewGuid(), name, description, price, stock)
        {
            _name = name;
            _description = description;
            _price = price;
            _stock = stock;
            CatalogType = catalogType;
        }

    }
}