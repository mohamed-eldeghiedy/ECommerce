using ECommerce.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<bool> DeleteAsync(string id);
        Task<CustomerBasket> GetAsync (string id);
        Task<CustomerBasket> CreateOrUpdateAsync(CustomerBasket basket, TimeSpan? TTL = null);  
    }
}
