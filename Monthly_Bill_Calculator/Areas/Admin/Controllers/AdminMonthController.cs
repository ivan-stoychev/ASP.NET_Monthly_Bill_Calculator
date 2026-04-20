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

        public IActionResult Index(string searchEmail, int? filterYear, int? filterMonth, bool? filterStatus, int page = 1)
        {
            int pageSize = 10;

            var query = dbContext.Months
                .Include(m => m.User)
                .Include(m => m.Electricity)
                .Include(m => m.ColdWater)
                .Include(m => m.HotWater)
                .Include(m => m.NaturalGas)
                .Include(m => m.Steam)
                .Include(m => m.CentralHeating)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchEmail))
                query = query.Where(m => m.User.Email.Contains(searchEmail));

            if (filterYear.HasValue)
                query = query.Where(m => m.Year == filterYear);

            if (filterMonth.HasValue)
                query = query.Where(m => m.MonthNumber == filterMonth);

            if (filterStatus.HasValue)
                query = query.Where(m => m.IsPaid == filterStatus.Value);

            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var months = query
                .OrderByDescending(m => m.Year)
                .ThenByDescending(m => m.MonthNumber)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;

            ViewData["CurrentEmail"] = searchEmail;
            ViewData["CurrentYear"] = filterYear;
            ViewData["CurrentMonth"] = filterMonth;
            ViewData["CurrentStatus"] = filterStatus;

            return View(months);
        }

        public IActionResult Create()
        {
            ViewBag.Users = dbContext.Users.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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