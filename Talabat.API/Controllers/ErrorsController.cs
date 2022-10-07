using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;

namespace Talabat.API.Controllers
{
    //[ApiController]
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi= true)]//true so swagger not Doc Cont so no err as must specify meth of Endp Errors
                                         
    public class ErrorsController 
    {
        public ActionResult Errors(int code)  
        {                                     
            return new ObjectResult(new ApiResponse(code));   
        }
    }
}
