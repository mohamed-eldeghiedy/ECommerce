using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DataTransfareObjects.Auth
{
    public record LoginRequest([EmailAddress] string Email, string Password);
}
