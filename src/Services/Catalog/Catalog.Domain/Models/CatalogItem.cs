using System;
using Catalog.Domain.Events;
using Catalog.Domain.Exceptions;
using Domain.Models;

namespace Catalog.Domain.Models
{
    public class CatalogItem : AggregateRoot<Guid>
    {
        private string _name;

        private string _description;

        private decimal _price;

        private int _stock;

        public virtual CatalogType CatalogType { get; private set; }
        private Guid _catalogTypeId;

        /// <summary>
        /// Initializes a new catalog item instance.
        /// </summary>
        /// <remarks>
        /// Add a <see cref="CatalogItemCreatedDomainEvent"/>.
        /// </remarks>
        /// <param name="name">The catalog item name.</param>
        /// <param name="description">The catalog item description.</param>
        /// <param name="price">The catalog item price.</param>
        /// <param name="stock">The catalog item stock.</param>
        /// <param name="catalogTypeId">The catalog item type identifier.</param>
        public CatalogItem(string name, string description, decimal price, int stock, Guid catalogTypeId)
            : base(Guid.NewGuid())
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new CatalogDomainException("The catalog item name cannot be null, empty or whitespace.");

            if (price < 0)
                throw new CatalogDomainException("The catalog item price cannot have negative value.");

            if (stock < 0)
                throw new CatalogDomainException("The catalog item stock cannot have negative value.");

            _name = name;
            _description = description;
            _price = price;
            _stock = stock;
            _catalogTypeId = catalogTypeId;

            AddDomainEvent(new CatalogItemCreatedDomainEvent(name, description, price, stock, catalogTypeId));
        }

        /// <summary>
        /// Sets catalog item name.
        /// </summary>
        /// <param name="name">The name to be changed.</param>
        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new CatalogDomainException("The catalog item name cannot be null, empty or whitespace.");

            _name = name;
        }

        /// <summary>
        /// Sets catalog item description.
        /// </summary>
        /// <param name="description">The description to be changed.</param>
        public void ChangeDescription(string description)
        {
            _description = description;
        }

        /// <summary>
        /// Sets catalog item price.
        /// </summary>
        /// <remarks>
        /// Add a <see cref="CatalogItemPriceChangedDomainEvent"/>.
        /// </remarks>
        /// <param name="price">The price to be changed.</param>
        public void ChangePrice(decimal price)
        {
            if (price < 0)
                throw new CatalogDomainException("The catalog item price cannot have negative value.");

            if (_price == price)
                return;

            _price = price;

            AddDomainEvent(new CatalogItemPriceChangedDomainEvent(price));
        }

        /// <summary>
        /// Sets catalog item stock.
        /// </summary>
        /// <remarks>
        /// Adds a <see cref="CatalogItemStockChangedDomainEvent"/>.
        /// </remarks>
        /// <param name="stock">The stock to be changed.</param>
        public void ChangeStock(int stock)
        {
            if (stock < 0)
                throw new CatalogDomainException("The catalog item stock cannot have negative value.");

            if (_stock == stock)
                return;

            _stock = stock;

            AddDomainEvent(new CatalogItemStockChangedDomainEvent(stock));
        }

        /// <summary>
        /// Sets catalog item type identifier.
        /// </summary>
        /// <param name="catalogTypeId">The catalog type to be changed.</param>
        public void ChangeCatalogTypeId(Guid catalogTypeId)
        {
            _catalogTypeId = catalogTypeId;
        }
    }
}