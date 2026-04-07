using Microsoft.AspNetCore.Mvc;

namespace Monthly_Bill_Calculator.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("NotFound");
            }

            return View("Error");
        }

        [Route("Error/500")]
        public IActionResult ServerError()
        {
            return View("ServerError");
        }

    }
}
