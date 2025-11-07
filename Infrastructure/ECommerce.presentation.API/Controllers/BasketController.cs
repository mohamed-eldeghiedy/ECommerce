using ECommerce.ServiceAbstraction;
using ECommerce.Shared.DataTransfareObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.presentation.API.Controllers
{
    public class BasketController(IBasketService  basketService) : APIBaseController
    {
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDTO>>  Update(CustomerBasketDTO basketDTO)
        {
            return Ok(await basketService.CreateOrUpdateAsync(basketDTO));
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDTO>> Get(string Id)
        {
            return Ok(await basketService.GetByIdAsync( Id));
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string Id)
        { 
            await basketService.DeleteAsync( Id);
            return  NoContent();
        }
    }
}
