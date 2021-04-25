using System;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Configurations;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Catalog.Infrastructure.Data
{
    public class CatalogDataContext : AppDbContext
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

    public class CatalogDataContextDesignFactory : IDesignTimeDbContextFactory<CatalogDataContext>
    {
        public CatalogDataContext CreateDbContext(string[] args)
        {
            var connectionString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory).GetConnectionString("CatalogSqlServer");

            var optionsBuilder = new DbContextOptionsBuilder<CatalogDataContext>()
                .UseSqlServer(connectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                    });

            return new CatalogDataContext(optionsBuilder.Options);
        }
    }
}