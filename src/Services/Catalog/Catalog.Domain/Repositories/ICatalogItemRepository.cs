using System;
using System.Threading.Tasks;
using Catalog.Domain.Models;
using Domain.Core.Data;

namespace Catalog.Domain.Repositories
{
    public interface ICatalogItemRepository : IRepository<CatalogItem, Guid>
    {
        Task<CatalogItem> GetCatalogItemAsync(Guid id);

        Task<CatalogItem> AddAsync(CatalogItem catalogItem);

        Task UpdateAsync(CatalogItem catalogItem);

        Task DeleteAsync(Guid id);
    }
}