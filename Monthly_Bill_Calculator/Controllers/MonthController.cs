using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.Data;
using Monthly_Bill_Calculator.DB_Models;
using System.Security.Claims;

namespace Monthly_Bill_Calculator.Controllers
{
    [Authorize]
    public class MonthController : Controller
    {
        private readonly CalcAppDbContext dbContext;

        public MonthController(CalcAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var months = dbContext.Months
                .Where(m => m.UserId == userId && !m.IsPaid)
                .Include(m => m.Electricity)
                .Include(m => m.ColdWater)
                .Include(m => m.HotWater)
                .Include(m => m.NaturalGas)
                .Include(m => m.Steam)
                .Include(m => m.CentralHeating)
                .OrderBy(m => m.Year)
                .ThenBy(m => m.MonthNumber)
                .AsNoTracking()
                .ToList();

            return View(months);
        }

        public IActionResult Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var month = dbContext.Months
                .Where(m => m.UserId == userId)
                .Include(m => m.Electricity)
                .Include(m => m.ColdWater)
                .Include(m => m.HotWater)
                .Include(m => m.NaturalGas)
                .Include(m => m.Steam)
                .Include(m => m.CentralHeating)
                .FirstOrDefault(m => m.Id == id);

            if (month == null)
                return NotFound();

            return View(month);
        }

        [HttpPost]
        public IActionResult MarkAsPaid(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var month = dbContext.Months
                .Where(m => m.UserId == userId)
                .FirstOrDefault(m => m.Id == id);

            if (month == null)
                return NotFound();

            month.IsPaid = true;
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}