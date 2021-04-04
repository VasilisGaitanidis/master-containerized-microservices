using System.Collections.Generic;

namespace Cart.Application.Dtos.Requests
{
    public class UpdateShoppingCartDto
    {
        public UpdateShoppingCartDto()
        {
            Items = new List<UpdateShoppingCartItemDto>();
        }

        /// <summary>
        /// The username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The items.
        /// </summary>
        public IEnumerable<UpdateShoppingCartItemDto> Items { get; set; }
    }
}