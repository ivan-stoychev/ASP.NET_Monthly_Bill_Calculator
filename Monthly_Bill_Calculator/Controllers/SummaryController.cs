using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.Data;
using Monthly_Bill_Calculator.Models;

namespace Monthly_Bill_Calculator.Controllers
{
    public class SummaryController : Controller
    {
        private readonly CalcAppDbContext dbContext;

        public SummaryController(CalcAppDbContext CalcApp)
        {
            this.dbContext = CalcApp;
        }

        public IActionResult Index()
        {
            return View(new SummaryViewModel());
        }

        [HttpPost]
        public IActionResult Index(SummaryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            int start = model.StartYear * 100 + model.StartMonth;
            int end = model.EndYear * 100 + model.EndMonth;

            if (start > end)
            {
                ModelState.AddModelError("", "Start date cannot be after end date.");
                return View(model);
            }

            var months = dbContext.Months
                .Include(m => m.Electricity)
                .Include(m => m.ColdWater)
                .Include(m => m.HotWater)
                .Include(m => m.NaturalGas)
                .Include(m => m.Steam)
                .Include(m => m.CentralHeating)
                .Where(m => (m.Year * 100 + m.MonthNumber) >= start &&
                            (m.Year * 100 + m.MonthNumber) <= end)
                .ToList();

            if (!months.Any())
            {
                ModelState.AddModelError("", "No data found in this date range.");
                return View(model);
            }

            model.Utilities = new Dictionary<string, UtilitySummary>
            {
                ["Electricity"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.Electricity?.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.Electricity?.Price ?? 0),
                    TotalConsumption = months.Sum(m => m.Electricity?.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.Electricity?.Price ?? 0)
                },

                ["Cold Water"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.ColdWater?.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.ColdWater?.Price ?? 0),
                    TotalConsumption = months.Sum(m => m.ColdWater?.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.ColdWater?.Price ?? 0)
                },

                ["Hot Water"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.HotWater?.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.HotWater?.Price ?? 0),
                    TotalConsumption = months.Sum(m => m.HotWater?.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.HotWater?.Price ?? 0)
                },

                ["Natural Gas"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.NaturalGas?.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.NaturalGas?.Price ?? 0),
                    TotalConsumption = months.Sum(m => m.NaturalGas?.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.NaturalGas?.Price ?? 0)
                },

                ["Steam"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.Steam?.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.Steam?.Price ?? 0),
                    TotalConsumption = months.Sum(m => m.Steam?.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.Steam?.Price ?? 0)
                },

                ["Central Heating"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.CentralHeating?.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.CentralHeating?.Price ?? 0),
                    TotalConsumption = months.Sum(m => m.CentralHeating?.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.CentralHeating?.Price ?? 0)
                }
            };

            model.TotalSpent = model.Utilities.Sum(u => u.Value.TotalPrice);

            return View(model);
        }
    }
}