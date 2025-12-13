using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransfareObjects.Auth
{
    public record RegisterRequest([EmailAddress] string Email, string DisplayName, string Password,
     string? UserName = "MMM", string? PhoneNumber = "");
}
