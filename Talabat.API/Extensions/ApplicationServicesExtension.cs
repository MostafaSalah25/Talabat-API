using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Repositories;
using Talabat.BLL.Services;

namespace Talabat.API.Extensions
{
    public static class ApplicationServicesExtension 
    {
        public static IServiceCollection AddApplicationServices( this IServiceCollection services) 
        {
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); 
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();

            services.AddAutoMapper(typeof(MappingProfiles));

            // Validation Error
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {   
                    
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Count > 0)
                                                         .SelectMany(M => M.Value.Errors)                           
                                                         .Select(E => E.ErrorMessage).ToArray(); 
                    var errorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };

            });
            return services;
        }
    }
}
