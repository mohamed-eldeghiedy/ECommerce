using ECommerce.ServiceAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.presentation.API.Attributes
{
    public class RedisCashAttribute(int durationInMin = 2) : ActionFilterAttribute
    {

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var cashService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();
            string Key = GenerateCashKey(context.HttpContext.Request);
            var cashValue = await cashService.GetAsync(Key);
            if (cashValue != null)
            {
                context.Result = new ContentResult
                {
                    Content = cashValue,
                    ContentType = "application/json",
                    StatusCode =  StatusCodes.Status200OK

                };
                return;            
            }

            var actionExecutedContext = await next.Invoke();
            var result = actionExecutedContext.Result;
            if(result is OkObjectResult okObject)
            {
                await cashService.SetAsync( Key , okObject.Value , TimeSpan.FromMinutes(durationInMin));
            }
        }

        private static string GenerateCashKey(HttpRequest request)
        {
           var sb= new StringBuilder();
            foreach (var Kvp in request.Query.OrderBy(q=>q.Key))
            {
                sb.Append($"{Kvp.Key}-{Kvp.Value}"); 
            }
            return sb.ToString().Trim('-');
        }
    }
}
