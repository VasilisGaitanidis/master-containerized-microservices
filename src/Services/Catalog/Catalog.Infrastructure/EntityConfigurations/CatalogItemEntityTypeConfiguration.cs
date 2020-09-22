using System;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityConfigurations
{
    public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("CatalogItems", CatalogDataContext.DefaultSchema);

            builder.HasKey(c => c.Id);

            builder.Ignore(c => c.DomainEvents);

            builder.Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Name")
                .IsRequired();

            builder.Property<string>("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Description")
                .IsRequired(false);

            builder.Property<decimal>("_price")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Price")
                .HasColumnType("decimal(18,4)")
                .IsRequired();

            builder.Property<int>("_stock")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Stock")
                .IsRequired();

            builder.HasOne(c => c.CatalogType)
                .WithMany()
                .HasForeignKey("_catalogTypeId");

            builder.Property<Guid>("_catalogTypeId")
                .HasColumnName("CatalogTypeId");
        }
    }
}