using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Basket;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repositories
{
    internal class BasketRepository(IConnectionMultiplexer multiplexer ) : IBasketRepository
    {
        private readonly IDatabase database = multiplexer.GetDatabase();
        public async Task<CustomerBasket> CreateOrUpdateAsync(CustomerBasket basket, TimeSpan? TTL = null)
        {
            var json = JsonSerializer.Serialize(basket);
            await database.StringSetAsync(basket.Id , json , TTL?? TimeSpan.FromDays(9) );
            return await GetAsync(basket.Id);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket> GetAsync(string id)
        {
            var json = await database.StringGetAsync(id);
            if (json.IsNullOrEmpty)
                return null;
            return JsonSerializer.Deserialize<CustomerBasket>(json!);
        }
    }
}
