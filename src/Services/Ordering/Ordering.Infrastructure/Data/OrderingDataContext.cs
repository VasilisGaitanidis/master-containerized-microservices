using System;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Configurations;

namespace Ordering.Infrastructure.Data
{
    public class OrderingDataContext : AppDbContext
    {
        /// <summary>
        /// The default database schema.
        /// </summary>
        public const string DefaultSchema = "ordering";

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Buyer> Buyers { get; set; }

        public OrderingDataContext(DbContextOptions<OrderingDataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BuyerEntityTypeConfiguration());
        }
    }

    public class OrderingDataContextDesignFactory : IDesignTimeDbContextFactory<OrderingDataContext>
    {
        public OrderingDataContext CreateDbContext(string[] args)
        {
            var connectionString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory).GetConnectionString("OrderingSqlServer");

            var optionsBuilder = new DbContextOptionsBuilder<OrderingDataContext>()
                .UseSqlServer(connectionString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                    });

            return new OrderingDataContext(optionsBuilder.Options);
        }
    }
}