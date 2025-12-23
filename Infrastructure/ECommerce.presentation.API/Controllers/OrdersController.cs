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
    public class OrdersController(IOrderService orderService ) : APIBaseController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Create(OrderRequest request , CancellationToken cancellationToken)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await orderService.CreateAsync(request, email , cancellationToken);
            return HandleResult(result);
        }

        [HttpGet("{id:guid}")]

        public async Task<ActionResult<OrderResponse>> Get(Guid id , CancellationToken cancellationToken)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await orderService.GetByIdAsync(email , id , cancellationToken );
            return HandleResult(result);
        }

        [HttpGet]

        public async Task<ActionResult<OrderResponse>> GetAll(CancellationToken cancellationToken)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await orderService.GetByUserEmailAsync(email , cancellationToken);
            return Ok(result);
        }

        [HttpGet("deliveryMethods")]

        public async Task<ActionResult<DeliveryMethodResponse>> GetDeliveryMethods(CancellationToken cancellationToken)
        {
            var result = await orderService.GetDeliveryMethodsAsync(cancellationToken);
            return Ok(result);
        }
    }
}
