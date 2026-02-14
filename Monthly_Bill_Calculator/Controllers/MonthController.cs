using Microsoft.AspNetCore.Mvc;

namespace Monthly_Bill_Calculator.Controllers
{
    public class MonthController : Controller
    {
        public IActionResult Index()
        {
            return this.Ok("I am at Month Controller");
        }
    }
}
