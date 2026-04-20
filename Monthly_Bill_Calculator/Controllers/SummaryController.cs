using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.Data;
using Monthly_Bill_Calculator.DB_Models;
using Monthly_Bill_Calculator.Models;
using System.Security.Claims;

namespace Monthly_Bill_Calculator.Controllers
{
    [Authorize]
    public class SummaryController : Controller
    {
        private readonly CalcAppDbContext db;

        public SummaryController(CalcAppDbContext db)
        {
            this.db = db;
        }

        public IActionResult AverageUsage()
        {
            return View(new SummaryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AverageUsage(SummaryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var months = db.Months
                .Include(m => m.Electricity)
                .Include(m => m.ColdWater)
                .Include(m => m.HotWater)
                .Include(m => m.NaturalGas)
                .Include(m => m.Steam)
                .Include(m => m.CentralHeating)
                .Where(m =>
                    m.UserId == userId &&
                    m.IsPaid &&
                    (m.Year > model.StartYear ||
                     (m.Year == model.StartYear && m.MonthNumber >= model.StartMonth)) &&
                    (m.Year < model.EndYear ||
                     (m.Year == model.EndYear && m.MonthNumber <= model.EndMonth))
                )
                .ToList();

            if (!months.Any())
            {
                model.Utilities = null;
                return View(model);
            }

            model.Utilities = new Dictionary<string, UtilitySummary>
            {
                ["Electricity"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.Electricity.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.Electricity?.MonthlyCost ?? 0)
                },
                ["Cold Water"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.ColdWater.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.ColdWater?.MonthlyCost ?? 0)
                },
                ["Hot Water"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.HotWater.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.HotWater?.MonthlyCost ?? 0)
                },
                ["Natural Gas"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.NaturalGas.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.NaturalGas?.MonthlyCost ?? 0)
                },
                ["Steam"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.Steam.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.Steam?.MonthlyCost ?? 0)
                },
                ["Central Heating"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.CentralHeating.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.CentralHeating?.MonthlyCost ?? 0)
                }
            };

            model.TotalAverageSpent = model.Utilities.Sum(u => u.Value.AvgPrice);

            return View(model);
        }

        public IActionResult TotalUsage()
        {
            return View(new SummaryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TotalUsage(SummaryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var months = db.Months
                .Include(m => m.Electricity)
                .Include(m => m.ColdWater)
                .Include(m => m.HotWater)
                .Include(m => m.NaturalGas)
                .Include(m => m.Steam)
                .Include(m => m.CentralHeating)
                .Where(m =>
                    m.UserId == userId &&
                    m.IsPaid &&
                    (m.Year > model.StartYear ||
                     (m.Year == model.StartYear && m.MonthNumber >= model.StartMonth)) &&
                    (m.Year < model.EndYear ||
                     (m.Year == model.EndYear && m.MonthNumber <= model.EndMonth))
                )
                .ToList();

            if (!months.Any())
            {
                model.Utilities = null;
                return View(model);
            }

            model.Utilities = new Dictionary<string, UtilitySummary>
            {
                ["Electricity"] = new UtilitySummary
                {
                    TotalConsumption = months.Sum(m => m.Electricity.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.Electricity?.MonthlyCost ?? 0)
                },
                ["Cold Water"] = new UtilitySummary
                {
                    TotalConsumption = months.Sum(m => m.ColdWater.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.ColdWater?.MonthlyCost ?? 0)
                },
                ["Hot Water"] = new UtilitySummary
                {
                    TotalConsumption = months.Sum(m => m.HotWater.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.HotWater?.MonthlyCost ?? 0)
                },
                ["Natural Gas"] = new UtilitySummary
                {
                    TotalConsumption = months.Sum(m => m.NaturalGas.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.NaturalGas?.MonthlyCost ?? 0)
                },
                ["Steam"] = new UtilitySummary
                {
                    TotalConsumption = months.Sum(m => m.Steam.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.Steam?.MonthlyCost ?? 0)
                },
                ["Central Heating"] = new UtilitySummary
                {
                    TotalConsumption = months.Sum(m => m.CentralHeating.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.CentralHeating?.MonthlyCost ?? 0)
                }
            };

            model.TotalSpent = model.Utilities.Sum(u => u.Value.TotalPrice);

            return View(model);
        }
    }
}