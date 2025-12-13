using ECommerce.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Contracts
{
    public interface ITokenService
    {
        string GetToken(ApplicationUser user , IList<string> roles);
    }
}
