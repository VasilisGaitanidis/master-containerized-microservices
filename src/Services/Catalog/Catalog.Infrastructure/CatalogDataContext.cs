using Catalog.Domain.Models;
using Catalog.Infrastructure.EntityConfigurations;
using Domain.Core.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure
{
    public class CatalogDataContext : AppDbContext, IUnitOfWork
    {
        /// <summary>
        /// The default database schema.
        /// </summary>
        public const string DefaultSchema = "catalog";

        public DbSet<CatalogItem> CatalogItems { get; set; }

        public DbSet<CatalogType> CatalogTypes { get; set; }

        public CatalogDataContext(DbContextOptions<CatalogDataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
        }
    }
}