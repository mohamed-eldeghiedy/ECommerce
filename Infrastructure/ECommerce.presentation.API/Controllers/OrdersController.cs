using ECommerce.ServiceAbstraction;
using ECommerce.Shared.DataTransfareObjects.UserOrder;
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
    public class OrdersController(IOrderService orderService) : APIBaseController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Create(OrderRequest request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await orderService.CreateAsync(request, email);
            return HandleResult(result);
        }
    }
}
