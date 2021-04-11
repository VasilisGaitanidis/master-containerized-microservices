using Application.Exceptions;

namespace Discount.Application.Exceptions
{
    public class CouponNotFoundException : EntityNotFoundException
    {
        public CouponNotFoundException(string productName)
            : base($"Coupon with product name '{productName}' not found.")
        {
            ProductName = productName;
        }

        public string ProductName { get; }
    }
}