using System.Threading.Tasks;
using Cart.Domain.Entities;

namespace Cart.Domain.Repositories
{
    /// <summary>
    /// The shopping cart repository.
    /// </summary>
    public interface ICartRepository
    {
        /// <summary>
        /// Get a shopping cart by a <paramref name="username"/>.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>A <see cref="ShoppingCart"/>.</returns>
        Task<ShoppingCart> GetShoppingCartAsync(string username);

        /// <summary>
        /// Update a shopping cart.
        /// </summary>
        /// <param name="cart">The shopping cart to be updated.</param>
        /// <returns>The updated <see cref="ShoppingCart"/>.</returns>
        Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart cart);

        /// <summary>
        /// Deletes a shopping cart by a <paramref name="username"/>.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        Task DeleteShoppingCartAsync(string username);
    }
}