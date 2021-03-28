using System;
using System.Threading.Tasks;
using Cart.Domain.Entities;
using Cart.Domain.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Cart.Infrastructure.Repositories
{
    /// <inheritdoc />
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _redisCache;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="redisCache"></param>
        public CartRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        /// <inheritdoc />
        public async Task<ShoppingCart> GetShoppingCartAsync(string username)
        {
            var shoppingCart = await _redisCache.GetStringAsync(username);

            return string.IsNullOrWhiteSpace(shoppingCart) ? null : JsonConvert.DeserializeObject<ShoppingCart>(shoppingCart);
        }

        /// <inheritdoc />
        public async Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart cart)
        {
            await _redisCache.SetStringAsync(cart.Username, JsonConvert.SerializeObject(cart));

            return await GetShoppingCartAsync(cart.Username);
        }

        /// <inheritdoc />
        public async Task DeleteShoppingCartAsync(string username)
            => await _redisCache.RemoveAsync(username);
    }
}