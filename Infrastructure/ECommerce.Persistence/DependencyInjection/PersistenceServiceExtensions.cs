using ECommerce.Domain.Contracts;
using ECommerce.Persistence.Context;
using ECommerce.Persistence.DbInitializers;
using ECommerce.Persistence.Repositories;
using ECommerce.Persistence.Services;
using ECommerce.ServiceAbstraction;



//using ECommerce.Persistence.DbInitializers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.DependencyInjection
{
    public static class PersistenceServiceExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connection = configuration.GetConnectionString("SqlConnection");
                options.UseSqlServer(connection);

            });

            services.AddSingleton<IConnectionMultiplexer>
            (cfg=>
            { 
            

                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
            
            });
            services.AddScoped<ICashService , CashService>();
            services.AddScoped<IBasketRepository , BasketRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            return services;
        }

    }
}
