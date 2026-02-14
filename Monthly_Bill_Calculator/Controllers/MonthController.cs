using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.Data;
using Monthly_Bill_Calculator.DB_Models;

namespace Monthly_Bill_Calculator.Controllers
{
    public class MonthController : Controller
    {
        private readonly CalcAppDbContext dbContext;
        public MonthController(CalcAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IActionResult Index()
        {
            return this.Ok("This is Month Controller");
        }
    }
}
