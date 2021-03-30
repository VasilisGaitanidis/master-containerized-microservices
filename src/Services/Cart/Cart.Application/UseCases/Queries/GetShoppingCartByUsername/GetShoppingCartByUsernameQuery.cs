using Cart.Application.Dtos.Responses;
using MediatR;

namespace Cart.Application.UseCases.Queries.GetShoppingCartByUsername
{
    public class GetShoppingCartByUsernameQuery : IRequest<ShoppingCartDto>
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="username">The shopping cart user's username.</param>
        public GetShoppingCartByUsernameQuery(string username)
        {
            Username = username;
        }

        /// <summary>
        /// The shopping cart user's username.
        /// </summary>
        public string Username { get; }
    }
}