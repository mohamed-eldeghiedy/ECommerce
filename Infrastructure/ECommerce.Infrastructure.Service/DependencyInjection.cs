using ECommerce.Service.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services , IConfiguration configuration)
        {
            services.Configure<JWTOptions>(cfg=>
            {
                configuration.GetSection(JWTOptions.SectionName);
            });
            services.AddScoped<ITokenService,TokenService>();
            return services;
        }
    }
}
