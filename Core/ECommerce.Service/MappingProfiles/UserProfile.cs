using AutoMapper;
using ECommerce.Domain.Entities.Auth;
using ECommerce.Shared.DataTransfareObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Address, AddressDTO>()
            .ReverseMap();
        }
    }
}
