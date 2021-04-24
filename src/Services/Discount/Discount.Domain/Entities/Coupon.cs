using Discount.Domain.Exceptions;
using System;

namespace Discount.Domain.Entities
{
    public class Coupon
    {
        /// <summary>
        /// Initializes a coupon entity.
        /// </summary>
        /// <param name="productName">The product name.</param>
        /// <param name="description">The coupon description.</param>
        /// <param name="amount">The amount.</param>
        public Coupon(string productName, string description, int amount)
        {
            ProductName = productName;
            Description = description;
            Amount = amount;
        }

        /// <summary>
        /// The coupon identifier.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The coupon product name.
        /// </summary>
        public string ProductName { get; protected set; }

        /// <summary>
        /// The coupon description.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// The coupon amount.
        /// </summary>
        public int Amount { get; protected set; }

        /// <summary>
        /// Sets coupon description.
        /// </summary>
        /// <param name="description">The new coupon description.</param>
        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new CouponDomainException($"The coupon '{nameof(description)}' cannot be null, empty or whitespace.",
                    new ArgumentException($"'{nameof(description)}' cannot be null or whitespace.", nameof(description)));
            }

            Description = description;
        }

        /// <summary>
        /// Sets coupon amount.
        /// </summary>
        /// <param name="amount">The new coupon amount.</param>
        public void ChangeAmount(int amount)
        {
            if (amount < 0)
            {
                throw new CouponDomainException("The coupon amount cannot have negative value.");
            }

            if (Amount == amount)
            {
                return;
            }

            Amount = amount;
        }
    }
}