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

        // GET - Average Usage Page
        public IActionResult AverageUsage()
        {
            return View("AverageUsage", new SummaryViewModel());
        }

        // GET - Total Usage Page
        public IActionResult TotalUsage()
        {
            return View("TotalUsage", new SummaryViewModel());
        }

        // POST - Calculate Summary (used by both Average and Total usage)
        private bool CalculateSummary(SummaryViewModel model)
        {
            if (!ModelState.IsValid)
                return false;

            int start = model.StartYear * 100 + model.StartMonth;
            int end = model.EndYear * 100 + model.EndMonth;

            if (start > end)
            {
                ModelState.AddModelError("", "Start date cannot be after end date.");
                return false;
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
                return false;
            }

            model.Utilities = new Dictionary<string, UtilitySummary>
            {
                ["Electricity"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.Electricity?.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.Electricity?.MonthlyCost ?? 0),
                    TotalConsumption = months.Sum(m => m.Electricity?.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.Electricity?.MonthlyCost ?? 0)
                },

                ["Cold Water"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.ColdWater?.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.ColdWater?.MonthlyCost ?? 0),
                    TotalConsumption = months.Sum(m => m.ColdWater?.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.ColdWater?.MonthlyCost ?? 0)
                },

                ["Hot Water"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.HotWater?.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.HotWater?.MonthlyCost ?? 0),
                    TotalConsumption = months.Sum(m => m.HotWater?.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.HotWater?.MonthlyCost ?? 0)
                },

                ["Natural Gas"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.NaturalGas?.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.NaturalGas?.MonthlyCost ?? 0),
                    TotalConsumption = months.Sum(m => m.NaturalGas?.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.NaturalGas?.MonthlyCost ?? 0)
                },

                ["Steam"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.Steam?.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.Steam?.MonthlyCost ?? 0),
                    TotalConsumption = months.Sum(m => m.Steam?.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.Steam?.MonthlyCost ?? 0)
                },

                ["Central Heating"] = new UtilitySummary
                {
                    AvgConsumption = months.Average(m => m.CentralHeating?.Consumption ?? 0),
                    AvgPrice = months.Average(m => m.CentralHeating?.MonthlyCost ?? 0),
                    TotalConsumption = months.Sum(m => m.CentralHeating?.Consumption ?? 0),
                    TotalPrice = months.Sum(m => m.CentralHeating?.MonthlyCost ?? 0)
                }
            };

            model.TotalAverageSpent = model.Utilities.Sum(u => u.Value.AvgPrice);
            model.TotalSpent = model.Utilities.Sum(u => u.Value.TotalPrice);

            return true;
        }

        // POST - Average Usage
        [HttpPost]
        public IActionResult AverageUsage(SummaryViewModel model)
        {
            if (!CalculateSummary(model))
                return View("AverageUsage", model);

            return View("AverageUsage", model);
        }

        // POST - Total Usage
        [HttpPost]
        public IActionResult TotalUsage(SummaryViewModel model)
        {
            if (!CalculateSummary(model))
                return View("TotalUsage", model);

            return View("TotalUsage", model);
        }
    }
}