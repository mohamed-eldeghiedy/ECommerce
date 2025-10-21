using ECommerce.Service.Services;
using ECommerce.ServiceAbstraction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.DependencyInjection
{
    public static class ApplicationServiceExtentions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService  , ProductService>();
            services.AddAutoMapper(typeof(ApplicationServiceExtentions).Assembly);
            return services;
        }
    }
}
