using Application.Exceptions;

namespace Cart.Application.Exceptions
{
    public class ShoppingCartItemNegativePriceException : ValidationAppException
    {
        public ShoppingCartItemNegativePriceException(string productName)
            : base($"Shopping cart item with product name '{productName}' cannot have negative price.")
        {
            ProductName = productName;
        }

        public string ProductName { get; }
    }
}