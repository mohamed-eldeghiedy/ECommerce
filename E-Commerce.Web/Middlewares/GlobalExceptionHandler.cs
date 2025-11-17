using E_Commerce.Web.Middlewares;
using ECommerce.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Middlewares
{
    public class GlobalExceptionHandler(RequestDelegate next , ILogger<GlobalExceptionHandler> logger)
    {
        public async Task InvokeAsync(HttpContext context )
        {
            try
            {
                await next.Invoke(context);
                await HandleNotFoundEndPointAsync (context);
            }
            catch (Exception ex) 
            {
                logger.LogError("Something Went Wrong {Message}" , ex.Message);

                var problem = new ProblemDetails
                {

                    Title = "Error Proceesing the HTTP Request",
                    Detail = ex.Message,
                    Instance = context.Request.Path,
                    Status = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    }
                };

                context.Response.StatusCode = problem.Status.Value;
                await context.Response.WriteAsJsonAsync(problem);
            
            }
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var problem = new ProblemDetails
                {
                    Title = "Error Proceesing the HTTP Request -End Point Not Found",
                    Detail = $"End Point{context.Request.Path} Was Not Found ",
                    Instance = context.Request.Path,
                    Status = StatusCodes.Status404NotFound
                };
                await context.Response.WriteAsJsonAsync(problem);

            }
        }
    }
}


public static class GlobalExceptionHandlerExtensions
{
    public static WebApplication UseCustomExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandler>();
        return app;
    }
}
