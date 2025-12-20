using ECommerce.ServiceAbstraction;
using ECommerce.Shared.DataTransfareObjects.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.presentation.API.Controllers
{
    public class AuthController(IAuthService authService) : APIBaseController
    {
        [HttpPost("Register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest registerRequest)
        {
            var result = await authService.RegisterAsync(registerRequest);
            return HandleResult(result);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest loginRequest)
        {
            var result = await authService.LoginAsync(loginRequest);
            return HandleResult(result);
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail([FromQuery] string email)
        {
            var result = await authService.CheckEmailAsync(email);
            return Ok(result);
        }
    }
}
