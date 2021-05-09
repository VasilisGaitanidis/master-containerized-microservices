using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Configurations
{
    public class BuyerEntityTypeConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.ToTable("Buyers", OrderingDataContext.DefaultSchema);

            builder.HasKey(t => t.Id);

            builder.Property(b => b.FirstName)
                .IsRequired();

            builder.Property(b => b.LastName)
                .IsRequired();

            builder.Property(b => b.Email)
                .IsRequired(false);

            builder.Property(b => b.Country)
                .IsRequired();

            builder.Property(b => b.State)
                .IsRequired();
            
            builder.Property(b => b.ZipCode)
                .IsRequired();

            builder.HasMany(b => b.Orders)
                .WithOne(o => o.Buyer);
        }
    }
}