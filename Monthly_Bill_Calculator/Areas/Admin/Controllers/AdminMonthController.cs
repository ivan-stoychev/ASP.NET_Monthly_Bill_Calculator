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
                .Include(m => m.User)
                .Include(m => m.Electricity)
                .Include(m => m.ColdWater)
                .Include(m => m.HotWater)
                .Include(m => m.NaturalGas)
                .Include(m => m.Steam)
                .Include(m => m.CentralHeating)
                .OrderByDescending(m => m.Year)
                .ThenByDescending(m => m.MonthNumber)
                .ToList();

            return View(months);
        }

        public IActionResult Create()
        {
            ViewBag.Users = dbContext.Users.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Month month)
        {
            month.Electricity ??= new Electricity();
            month.ColdWater ??= new ColdWater();
            month.HotWater ??= new HotWater();
            month.NaturalGas ??= new NaturalGas();
            month.Steam ??= new Steam();
            month.CentralHeating ??= new CentralHeating();

            month.IsPaid = false;

            dbContext.Months.Add(month);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var month = dbContext.Months
                .Include(m => m.User)
                .Include(m => m.Electricity)
                .Include(m => m.ColdWater)
                .Include(m => m.HotWater)
                .Include(m => m.NaturalGas)
                .Include(m => m.Steam)
                .Include(m => m.CentralHeating)
                .FirstOrDefault(m => m.Id == id);

            if (month == null)
                return NotFound();

            ViewBag.Users = dbContext.Users.ToList();
            return View(month);
        }

        [HttpPost]
        public IActionResult Edit(Month month)
        {
            dbContext.Months.Update(month);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var month = dbContext.Months
                .Include(m => m.User)
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
                .Include(m => m.User)
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