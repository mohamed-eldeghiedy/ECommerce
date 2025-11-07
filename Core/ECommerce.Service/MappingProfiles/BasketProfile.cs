using AutoMapper;
using ECommerce.Domain.Entities.Basket;
using ECommerce.Shared.DataTransfareObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketItem , BasketItemDTO>().ReverseMap();

            CreateMap<CustomerBasket ,  CustomerBasketDTO>().ReverseMap();
        }
    }
}
