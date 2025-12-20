using AutoMapper;
using ECommerce.Domain.Entities.Auth;
using ECommerce.Service.Contracts;
using ECommerce.ServiceAbstraction;
using ECommerce.ServiceAbstraction.Common;
using ECommerce.Shared.DataTransfareObjects.Auth;
using ECommerce.Shared.DataTransfareObjects.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class UserService(UserManager<ApplicationUser> userManager ,
        ITokenService tokenService ,
        IMapper mapper) : IUserService
    {
        public async Task<Result<UserResponse>> GetByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return Error.NotFound("User not found" , $"User With Email{email} Was Not Found");
            var roles = await userManager.GetRolesAsync(user);
            return new UserResponse
            (
                user.Email,
                user.DisplayName,
                tokenService.GetToken(user , roles)
            );

        }
        public async Task<Result<AddressDTO>> GetAddressAsync(string email)
        {
           var user = await userManager.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(x=>x.Email == email);
            if (user == null)
                return Error.NotFound("User not found", $"User With Email{email} Was Not Found");
            if (user.Address == null)
                return Error.NotFound("Address not found", $"User With Email{email} doesn't have Address");
            return mapper.Map<AddressDTO>(user.Address);
        }

        

        public async Task<Result<AddressDTO>> UpdateAddressAsync(string email, AddressDTO addressDTO)
        {
            var user = await userManager.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return Error.NotFound("User not found", $"User With Email{email} Was Not Found");
            if (user.Address is not null)
            {
                user.Address.FirstName = addressDTO.FirstName;
                user.Address.LastName = addressDTO.LastName;
                user.Address.Street = addressDTO.Street;
                user.Address.City = addressDTO.City;
                user.Address.Country = addressDTO.Country;
            }
            else
            {
                user.Address = mapper.Map<Address>(addressDTO);
            }
            await userManager.UpdateAsync(user);
            return mapper.Map<AddressDTO>(user.Address);
        }
    }
}
