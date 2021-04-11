using System;
using System.Linq;
using System.Threading.Tasks;
using Discount.Domain.Entities;
using Discount.Domain.Repositories;
using Discount.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Discount.Infrastructure.Repositories
{
    /// <inheritdoc />
    public class CouponRepository : ICouponRepository
    {
        private readonly DiscountDataContext _context;

        public CouponRepository(DiscountDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc />
        public async Task<Coupon> GetAsync(int id)
        {
            var coupon = await _context.Coupons
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return coupon;
        }

        /// <inheritdoc />
        public async Task<Coupon> AddAsync(Coupon coupon)
        {
            var entityEntry = await _context.Coupons.AddAsync(coupon);

            return entityEntry.Entity;
        }

        /// <inheritdoc />
        public void Update(Coupon coupon)
        {
            _context.Entry(coupon).State = EntityState.Modified;

            _context.Update(coupon);
        }

        /// <inheritdoc />
        public void Delete(Coupon coupon)
        {
            _context.Entry(coupon).State = EntityState.Deleted;

            _context.Coupons.Remove(coupon);
        }
    }
}