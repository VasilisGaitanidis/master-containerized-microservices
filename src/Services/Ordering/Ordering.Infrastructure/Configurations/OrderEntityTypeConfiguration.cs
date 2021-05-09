using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Configurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", OrderingDataContext.DefaultSchema);

            builder.HasKey(o => o.Id);

            builder.Ignore(o => o.DomainEvents);

            builder.HasIndex(o => o.Username)
                .IsUnique(false);

            builder.Property(o => o.TotalPrice)
                .IsRequired();

            builder.Property(o => o.ShippingAddress)
                .IsRequired();

            builder.HasOne(o => o.Buyer)
                .WithMany(b => b.Orders)
                .HasForeignKey("BuyerId")
                .IsRequired();

            builder.HasMany(o => o.Items)
                .WithOne(oi => oi.Order);
        }
    }
}