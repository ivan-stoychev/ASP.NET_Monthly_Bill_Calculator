using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.Data;
using Monthly_Bill_Calculator.DB_Models;

namespace Monthly_Bill_Calculator.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminMonthController : Controller
    {
        private readonly CalcAppDbContext dbContext;

        public AdminMonthController(CalcAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var months = dbContext.Months
                .Include(m => m.Electricity)
                .Include(m => m.ColdWater)
                .Include(m => m.HotWater)
                .Include(m => m.NaturalGas)
                .Include(m => m.Steam)
                .Include(m => m.CentralHeating)
                .OrderBy(m => m.Year)
                .ThenBy(m => m.MonthNumber)
                .AsNoTracking()
                .ToArray();

            return View(months);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Month month)
        {
            if (!ModelState.IsValid)
                return View(month);

            dbContext.Months.Add(month);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var month = dbContext.Months
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
        public IActionResult Edit(Month month)
        {
            if (!ModelState.IsValid)
                return View(month);

            dbContext.Update(month);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var month = dbContext.Months
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

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var month = dbContext.Months.Find(id);

            if (month != null)
            {
                dbContext.Months.Remove(month);
                dbContext.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var month = dbContext.Months
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
    }
}