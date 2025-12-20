using ECommerce.ServiceAbstraction.Common;
using ECommerce.Shared.DataTransfareObjects.Auth;
using ECommerce.Shared.DataTransfareObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ServiceAbstraction
{
    public interface IUserService
    {
        Task<Result<UserResponse>> GetByEmailAsync(string email);
        Task<Result<AddressDTO>> GetAddressAsync(string email);
        Task<Result<AddressDTO>> UpdateAddressAsync(string email, AddressDTO addressDTO);
    }
}
