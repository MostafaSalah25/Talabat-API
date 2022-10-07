using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;

namespace Talabat.API.Helpers
{
    public class CashedResponse : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLive;

        public CashedResponse(int timeToLive)
        {
           _timeToLive = timeToLive;
        }
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           
             
            var cashedService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var casheKey = GenerateCashedKeyFromRequest(context.HttpContext.Request);
            var cashedResponse = await cashedService.GetCashedResponse(casheKey);

            if(!string.IsNullOrEmpty(cashedResponse)) 
            {
                var contentResult = new ContentResult()
                {
                    Content = cashedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return; 
            }

            var executedEndpointContext =  await next(); 

            if (executedEndpointContext.Result is OkObjectResult okObjectResult) 
                await cashedService.CasheResponceAsync(casheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLive));
        }
        private string GenerateCashedKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}"); 

            foreach (var (key,value)  in request.Query.OrderBy( X=> X.Key)) 
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
