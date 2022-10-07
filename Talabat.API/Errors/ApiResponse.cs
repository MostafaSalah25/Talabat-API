namespace Talabat.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statusCode , string message =null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultStatusCodeMessage(statusCode); 
        }
        private string GetDefaultStatusCodeMessage(int statusCode)

            => statusCode switch 
            {
                400 => "A Bad Request , You Have Made" ,
                401 => "Authorized , You Are Not" ,
                404 => "Resource Was Not Found" ,
                405 => "Method Not Allowed",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change. " ,
                _ => null    
            };

    }

}
