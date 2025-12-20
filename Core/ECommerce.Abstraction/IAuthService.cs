using ECommerce.ServiceAbstraction.Common;
using ECommerce.Shared.DataTransfareObjects.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ServiceAbstraction
{
    public interface IAuthService
    {
        Task<Result<UserResponse>> LoginAsync(LoginRequest loginRequest);
        Task<Result<UserResponse>> RegisterAsync(RegisterRequest registerRequest);
        Task<bool> CheckEmailAsync(string email);
    }
}
