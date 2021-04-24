using System;
using System.Threading.Tasks;
using Discount.Grpc.Protos;

namespace Cart.Infrastructure.Services.Grpc
{
    /// <inheritdoc />
    public class DiscountGrpcService : IDiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        /// <summary>
        /// Initializes a new instance of <see cref="DiscountGrpcService"/>.
        /// </summary>
        /// <param name="discountProtoService">The discount proto service client.</param>
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
        }

        /// <inheritdoc />
        public async Task<CouponResponse> GetCouponAsync(string productName)
        {
            var discountRequest = new GetCouponRequest { ProductName = productName };

            return await _discountProtoService.GetCouponAsync(discountRequest);
        }
    }
}