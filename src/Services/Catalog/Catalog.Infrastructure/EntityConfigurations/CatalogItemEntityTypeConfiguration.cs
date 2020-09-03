using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityConfigurations
{
    public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("catalog_items");

            builder.HasKey(c => c.Id);

            builder.Ignore(c => c.DomainEvents);

            builder.Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name")
                .IsRequired();

            builder.Property<string>("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("description")
                .IsRequired(false);

            builder.Property<decimal>("_price")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("price")
                .IsRequired();

            builder.Property<int>("_stock")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("stock")
                .IsRequired();

            builder.HasOne(c => c.CatalogType)
                .WithMany()
                .HasForeignKey("_catalogTypeId");
        }
    }
}