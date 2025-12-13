using ECommerce.Domain.Entities.Auth;
using ECommerce.Service.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Service
{
    public class TokenService(IOptions<JWTOptions> options) : ITokenService
    {
        public string GetToken(ApplicationUser user, IList<string> roles)
        {
            var Jwt = options.Value;
            List<Claim> clams =
                [
                    new(JwtRegisteredClaimNames.Name , user.DisplayName),
                    new (JwtRegisteredClaimNames.Email , user.Email)

                ];

            foreach (var role in roles)
            {
                clams.Add(new Claim(ClaimTypes.Role, role));
            }

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwt.Key));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer:Jwt.Issuer,
                audience: Jwt.Audience,
                claims: clams,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
