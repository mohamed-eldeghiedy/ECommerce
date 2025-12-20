using ECommerce.ServiceAbstraction;
using ECommerce.Shared.DataTransfareObjects.Auth;
using ECommerce.Shared.DataTransfareObjects.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.presentation.API.Controllers
{
    [Authorize]
    public class UsersController(IUserService UserService) : APIBaseController
    {
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetUser()
        {
            string email = User.FindFirstValue(ClaimTypes.Email)!;
            var result = await UserService.GetByEmailAsync(email);
            return HandleResult(result);
        }

        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            string email = User.FindFirstValue(ClaimTypes.Email)!;
            var result = await UserService.GetAddressAsync(email);
            return HandleResult(result);

        }

        [HttpPut]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO address)
        {
            string email = User.FindFirstValue(ClaimTypes.Email)!;
            var result = await UserService.UpdateAddressAsync(email , address);
            return HandleResult(result);
        }
    }
}

   
    
