using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.Data;
using Monthly_Bill_Calculator.DB_Models;
using System.Security.Claims;

namespace Monthly_Bill_Calculator.Controllers
{
    [Authorize]
    public class SummaryController : Controller
    {
        private readonly CalcAppDbContext dbContext;

        public SummaryController(CalcAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            IQueryable<Month> query;

            if (User.IsInRole("Admin"))
            {
                query = dbContext.Months.Where(m => m.IsPaid);
            }
            else
            {
                query = dbContext.Months.Where(m => m.UserId == userId && m.IsPaid);
            }

            var months = query
                .Include(m => m.Electricity)
                .Include(m => m.ColdWater)
                .Include(m => m.HotWater)
                .Include(m => m.NaturalGas)
                .Include(m => m.Steam)
                .Include(m => m.CentralHeating)
                .OrderBy(m => m.Year)
                .ThenBy(m => m.MonthNumber)
                .ToList();

            return View(months);
        }
    }
}