using Microsoft.AspNetCore.Mvc;
using Monthly_Bill_Calculator.Data;

namespace Monthly_Bill_Calculator.Controllers
{
    public class MonthController : Controller
    {
        private readonly CalcAppDbContext dbContext;
        public MonthController(CalcAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //public IActionResult Index()
        //{
        //    return this.Ok("I am at Month Controller");
        //}
    }
}
