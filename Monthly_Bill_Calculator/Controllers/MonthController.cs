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

        // GET - All "Month" objects from DB
        public IActionResult Index()
        {
            var months = dbContext.Months
                .Include(m => m.Electricity)
                .Include(m => m.ColdWater)
                .Include(m => m.HotWater)
                .Include(m => m.NaturalGas)
                .Include(m => m.Steam)
                .Include(m => m.CentralHeating)
                .AsNoTracking()
                .ToArray();

            return View(months);
        }

        // GET - Specific "Month" object
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

        // GET - Shows the form for new "Month" object
        public IActionResult Create()
        {
            return View();
        }

        // POST - Creates new "Month" object if valid
        [HttpPost]
        public IActionResult Create(Month month)
        {
            if (!ModelState.IsValid)
                return View(month);

            dbContext.Months.Add(month);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET - Loads the specific "Month" object to be edited
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

        // POST - Edits values of the specified "Month" object
        [HttpPost]
        public IActionResult Edit(Month month)
        {
            if (!ModelState.IsValid)
                return View(month);

            dbContext.Update(month);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET - Loads the specified "Month" object to be deleted
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

        // POST - Deletes the specified "Month" object
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
    }
}