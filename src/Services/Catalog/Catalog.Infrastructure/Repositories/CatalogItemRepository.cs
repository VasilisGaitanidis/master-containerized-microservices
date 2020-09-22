using System;
using System.Collections.Generic;
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

        public async Task<List<CatalogItem>> GetCatalogItemsAsync()
        {
            var catalogItems = await _context.CatalogItems
                .ToListAsync();

            return catalogItems;
        }

        public async Task<CatalogItem> AddAsync(CatalogItem catalogItem)
        {
            var entityEntry = await _context.CatalogItems.AddAsync(catalogItem);

            return entityEntry.Entity;
        }

        public void Update(CatalogItem catalogItem)
        {
            _context.Entry(catalogItem).State = EntityState.Modified;

            _context.Update(catalogItem);
        }

        public void Delete(CatalogItem catalogItem)
        {
            _context.Entry(catalogItem).State = EntityState.Deleted;

            _context.CatalogItems.Remove(catalogItem);
        }
    }
}