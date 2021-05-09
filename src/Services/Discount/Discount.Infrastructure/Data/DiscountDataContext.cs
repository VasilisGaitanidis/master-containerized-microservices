using System;
using Discount.Domain.Entities;
using Discount.Infrastructure.Configurations;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Discount.Infrastructure.Data
{
    public class DiscountDataContext : AppDbContext
    {
        /// <summary>
        /// The default database schema.
        /// </summary>
        public const string DefaultSchema = "discount";

        public DbSet<Coupon> Coupons { get; set; }

        public DiscountDataContext(DbContextOptions<DiscountDataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CouponEntityTypeConfiguration());
        }
    }

    public class DiscountDataContextDesignFactory : IDesignTimeDbContextFactory<DiscountDataContext>
    {
        public DiscountDataContext CreateDbContext(string[] args)
        {
            var connectionString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory).GetConnectionString("DiscountSqlServer");

            var optionsBuilder = new DbContextOptionsBuilder<DiscountDataContext>()
                .UseSqlServer(connectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                    });

            return new DiscountDataContext(optionsBuilder.Options);
        }
    }
}