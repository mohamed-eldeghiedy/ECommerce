using ECommerce.Domain.Entities.OrderEntities;
using ECommerce.Shared.DataTransfareObjects.UserOrder;
using ECommerce.Shared.DataTransfareObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    internal class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.DeliveryMethod,
                    opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.DeliveryMethodCost,
                    opt => opt.MapFrom(src => src.DeliveryMethod.Price))
                .ForMember(dest => dest.Total,
                    opt => opt.MapFrom(src => src.DeliveryMethod.Price + src.Subtotal));

            CreateMap<OrderAddress , AddressDTO>()
                .ReverseMap();

            CreateMap<OrderItem , OrderItemDTO>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.PictureUrl,
                    opt => opt.MapFrom(src => src.Product.PictureUrl));

            CreateMap<DeliveryMethod, DeliveryMethodResponse>()
                .ForMember(d=>d.Cost , o=>o.MapFrom(s=>s.Price) );
        }
    }
}
