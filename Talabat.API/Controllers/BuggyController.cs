using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.DAL;

namespace Talabat.API.Controllers
{
    // this controller is just for test errors 'see Response and edit it'
    public class BuggyController : BaseApiController 
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context )
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFound()
        {
            var product = _context.Products.Find(20);
            if (product == null)
                //return NotFound(); // see Error Response before y create ApiResponse
                  return NotFound(new ApiResponse(404));
            return Ok(product);
        }
        [HttpGet("badrequest")] // 400 Bad Request as Error Occured During Creating The Order
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("servererror")] //  500 Internal Server Error
        public ActionResult GetServerError()
        {
            var product = _context.Products.Find(100); 
            var producReturned = product.ToString(); 
            return Ok();
        }

        [HttpGet("badrequest/{id}")] // 400Bad Request 'validation errors'
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
