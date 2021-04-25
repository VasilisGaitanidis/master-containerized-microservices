using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discount.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Discount.Infrastructure.Data
{
    public class DiscountDataContextSeed
    {
        #region Demo entities

        private static readonly Coupon DemoCoupon1 = new Coupon("Demo1", "Demo product description 1", 6);
        private static readonly Coupon DemoCoupon2 = new Coupon("Demo2", "Demo product description 2", 4);

        #endregion

        public static async Task SeedAsync(DiscountDataContext discountDataContext, ILogger<DiscountDataContextSeed> logger)
        {
            if (!discountDataContext.Coupons.Any())
            {
                discountDataContext.Coupons.AddRange(GetPreconfiguredCoupons());

                await discountDataContext.SaveChangesAsync();

                logger.LogInformation("Seed database associated with context {DbContextName}", nameof(DiscountDataContext));
            }
        }

        private static IEnumerable<Coupon> GetPreconfiguredCoupons()
        {
            return new List<Coupon>
            {
                DemoCoupon1,
                DemoCoupon2
            };
        }
    }
}
