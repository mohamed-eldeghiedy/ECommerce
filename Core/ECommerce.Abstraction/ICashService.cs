using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ServiceAbstraction
{
    public interface ICashService
    {
        Task<string?> GetAsync(string key);
        Task SetAsync (string key, object value , TimeSpan TTL);
    }
}
