using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Configurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems", OrderingDataContext.DefaultSchema);

            builder.HasKey(t => t.Id);

            builder.Property(oi => oi.Quantity)
                .IsRequired();

            builder.Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(oi => oi.ProductName)
                .IsRequired();

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey("OrderId")
                .IsRequired();
        }
    }
}