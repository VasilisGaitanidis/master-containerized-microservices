using System.Threading.Tasks;
using Discount.Domain.Entities;

namespace Discount.Domain.Repositories
{
    /// <summary>
    /// Repository interface for coupon entity.
    /// </summary>
    public interface ICouponRepository
    {
        /// <summary>
        /// Get coupon by a <paramref name="productName"/>.
        /// </summary>
        /// <param name="productName">The coupon product name.</param>
        /// <returns>A <see cref="Coupon"/> entity.</returns>
        Task<Coupon> GetByProductNameAsync(string productName);

        /// <summary>
        /// Add a coupon.
        /// </summary>
        /// <param name="coupon">The coupon entity.</param>
        /// <returns>The newly added coupon.</returns>
        Task<Coupon> AddAsync(Coupon coupon);

        /// <summary>
        /// Update a coupon.
        /// </summary>
        /// <param name="coupon">The coupon entity.</param>
        void Update(Coupon coupon);

        /// <summary>
        /// Delete a coupon.
        /// </summary>
        /// <param name="coupon">The coupon entity.</param>
        void Delete(Coupon coupon);
    }
}