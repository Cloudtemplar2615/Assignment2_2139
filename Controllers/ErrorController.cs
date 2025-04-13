using Microsoft.AspNetCore.Mvc;

namespace Assignment_1_G_92_2139.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleError(int statusCode)
        {
            return statusCode switch
            {
                404 => View("Error404"),
                500 => View("Error500"),
                _ => View("Error500")
            };
        }
    }
}

