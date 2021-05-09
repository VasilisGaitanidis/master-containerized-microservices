using Cart.Application.Dtos.Requests;
using MediatR;

namespace Cart.Application.UseCases.Commands.CheckoutShoppingCart
{
    public class CheckoutShoppingCartCommand : IRequest<Unit>
    {
        public CheckoutShoppingCartCommand(string username, decimal totalPrice, string shippingAddress, BuyerDto buyer)
        {
            Username = username;
            TotalPrice = totalPrice;
            ShippingAddress = shippingAddress;
            Buyer = buyer;
        }

        public string Username { get; }

        public decimal TotalPrice { get; }

        public string ShippingAddress { get; }

        public BuyerDto Buyer { get; }
    }
}