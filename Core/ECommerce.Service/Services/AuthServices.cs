using ECommerce.Domain.Entities.Auth;
using ECommerce.Service.Contracts;
using ECommerce.ServiceAbstraction;
using ECommerce.ServiceAbstraction.Common;
using ECommerce.Shared.DataTransfareObjects.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    internal class AuthServices(UserManager<ApplicationUser> userManager , ITokenService tokenService ) : IAuthService
    {
        public async Task<Result<UserResponse>> LoginAsync(LoginRequest loginRequest)
        {
           var user = await userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
                return Error.Unauthorized(description: "Invalid Email Or Password");
            var result = await userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!result)
                return Error.Unauthorized(description: "Invalid Email Or Password");
            var roles = await userManager.GetRolesAsync(user);

            var token = tokenService.GetToken(user , roles);
            return new UserResponse(user.Email , user.DisplayName , token);
        }

        public async Task<Result<UserResponse>> RegisterAsync(RegisterRequest registerRequest)
        {
            var user = new ApplicationUser
            {
                Email = registerRequest.Email,
                DisplayName = registerRequest.DisplayName,
                UserName = registerRequest.UserName ?? registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber
            };
            var result = await userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
                return result.Errors.Select(e => Error.Vailedation(e.Code, e.Description)).ToList();
            var token = tokenService.GetToken(user , []);
            return new UserResponse(user.Email, user.DisplayName,token); 
            
        }
    }
}
