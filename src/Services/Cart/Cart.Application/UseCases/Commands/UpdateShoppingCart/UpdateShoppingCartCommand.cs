using System.Collections.Generic;
using Cart.Application.Dtos.Requests;
using Cart.Application.Dtos.Responses;
using MediatR;

namespace Cart.Application.UseCases.Commands.UpdateShoppingCart
{
    public class UpdateShoppingCartCommand : IRequest<ShoppingCartDto>
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="items">The items.</param>
        public UpdateShoppingCartCommand(string username, IEnumerable<UpdateShoppingCartItemDto> items)
        {
            Username = username;
            Items = items;
        }

        /// <summary>
        /// The username.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The shopping cart items.
        /// </summary>
        public IEnumerable<UpdateShoppingCartItemDto> Items { get; }
    }
}