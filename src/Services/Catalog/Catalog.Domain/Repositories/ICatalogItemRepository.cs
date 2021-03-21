using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Domain.Models;
using Domain.Data;

namespace Catalog.Domain.Repositories
{
    public interface ICatalogItemRepository : IRepository<CatalogItem, Guid>
    {
        Task<CatalogItem> GetCatalogItemAsync(Guid id);

        Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync();

        Task<CatalogItem> AddAsync(CatalogItem catalogItem);

        void Update(CatalogItem catalogItem);

        void Delete(CatalogItem catalogItem);
    }
}