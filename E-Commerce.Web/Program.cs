using E_Commerce.Web.Handlers;
using E_Commerce.Web.Middlewares;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Service;
using ECommerce.Persistence.DependencyInjection;
using ECommerce.Service.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(JWTOptions.SectionName));
            builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
            builder.Services.AddProblemDetails();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );
                    var problems = new ProblemDetails
                    {
                        
                        Title = "Validation Error",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "See the errors property for details.",
                        Extensions = { { "errors", errors } }
                    };
                    return new BadRequestObjectResult(problems);
                    
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(options=>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>
            {
                var Jwt = builder.Configuration.GetSection(JWTOptions.SectionName).Get<JWTOptions>();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Jwt.Issuer,
                    ValidAudience = Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                    


                };
            });
            var app = builder.Build();


            var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initializer.InitializerAsync();
            await initializer.InitializerAuthDbAsync();

            app.UseExceptionHandler();
            app.UseCustomExceptionHandler();    
            //app.UseMiddleware<GlobalExceptionHandler>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
