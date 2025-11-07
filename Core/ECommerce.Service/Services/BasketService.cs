using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Basket;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.DataTransfareObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<CustomerBasketDTO> CreateOrUpdateAsync(CustomerBasketDTO basketDTO)
        {
            var basket = mapper.Map<CustomerBasket>(basketDTO);
            var updateBasket = await basketRepository.CreateOrUpdateAsync(basket);
            return mapper.Map<CustomerBasketDTO>(updateBasket);
        }

        public Task DeleteAsync(string id)
        => basketRepository.DeleteAsync(id);

        public async Task<CustomerBasketDTO> GetByIdAsync(string id)
        {
            var basket = await basketRepository.GetAsync(id);
            return mapper.Map<CustomerBasketDTO>(basket); 
        }
    }
}
