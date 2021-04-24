using Discount.Domain.Entities;
using Discount.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discount.Infrastructure.Configurations
{
    public class CouponEntityTypeConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("Coupons", DiscountDataContext.DefaultSchema);

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.ProductName)
                .IsUnique();

            builder.Property(x => x.Amount)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired(false);
        }
    }
}