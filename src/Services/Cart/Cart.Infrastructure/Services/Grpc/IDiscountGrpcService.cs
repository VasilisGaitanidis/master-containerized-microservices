using System.Threading.Tasks;
using Discount.Grpc.Protos;

namespace Cart.Infrastructure.Services.Grpc
{
    /// <summary>
    /// The discount gRPC service.
    /// </summary>
    public interface IDiscountGrpcService
    {
        /// <summary>
        /// Get coupon based on the <paramref name="productName"/>.
        /// </summary>
        /// <param name="productName">The discount product name.</param>
        /// <returns>A <see cref="CouponResponse"/>.</returns>
        Task<CouponResponse> GetCouponAsync(string productName);
    }
}