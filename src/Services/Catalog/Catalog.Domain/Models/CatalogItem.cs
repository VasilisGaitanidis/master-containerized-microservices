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
        /// Initializes a new catalog item instance.
        /// </summary>
        /// <param name="name">The catalog item name.</param>
        /// <param name="description">The catalog item description.</param>
        /// <param name="price">The catalog item price.</param>
        /// <param name="stock">The catalog item stock.</param>
        /// <param name="catalogTypeId">The catalog item type identifier.</param>
        public CatalogItem(string name, string description, decimal price, int stock, Guid catalogTypeId)
            : base(Guid.NewGuid())
        {
            _name = name;
            _description = description;
            _price = price;
            _stock = stock;
            _catalogTypeId = catalogTypeId;
        }

    }
}