using System;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Models;
using Catalog.Domain.Repositories;
using Domain.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly CatalogDataContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public CatalogItemRepository(CatalogDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CatalogItem> GetCatalogItemAsync(Guid id)
        {
            var catalogItem = await _context.CatalogItems
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return catalogItem;
        }

        public Task<CatalogItem> AddAsync(CatalogItem catalogItem)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CatalogItem catalogItem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}