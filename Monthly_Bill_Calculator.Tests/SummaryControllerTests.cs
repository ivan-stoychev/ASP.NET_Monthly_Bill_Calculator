using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.Controllers;
using Monthly_Bill_Calculator.Data;
using Monthly_Bill_Calculator.DB_Models;
using System.Security.Claims;

namespace Monthly_Bill_Calculator.Tests
{
    [TestFixture]
    public class SummaryControllerTests
    {
        private CalcAppDbContext _context;
        private SummaryController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CalcAppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new CalcAppDbContext(options);

            var emptyUtilities1 = new
            {
                Elec = new Electricity { Consumption = 100, Price = 2 },
                Water = new ColdWater { Consumption = 0, Price = 0 },
                Hot = new HotWater { Consumption = 0, Price = 0 },
                Gas = new NaturalGas { Consumption = 0, Price = 0 },
                Steam = new Steam { Consumption = 0, Price = 0 },
                Heat = new CentralHeating { Consumption = 0, Price = 0 }
            };

            var emptyUtilities2 = new
            {
                Elec = new Electricity { Consumption = 200, Price = 2 },
                Water = new ColdWater { Consumption = 0, Price = 0 },
                Hot = new HotWater { Consumption = 0, Price = 0 },
                Gas = new NaturalGas { Consumption = 0, Price = 0 },
                Steam = new Steam { Consumption = 0, Price = 0 },
                Heat = new CentralHeating { Consumption = 0, Price = 0 }
            };

            var m1 = new Month
            {
                UserId = "u1",
                IsPaid = true,
                Year = 2024,
                MonthNumber = 1,
                Electricity = emptyUtilities1.Elec,
                ColdWater = emptyUtilities1.Water,
                HotWater = emptyUtilities1.Hot,
                NaturalGas = emptyUtilities1.Gas,
                Steam = emptyUtilities1.Steam,
                CentralHeating = emptyUtilities1.Heat
            };

            var m2 = new Month
            {
                UserId = "u1",
                IsPaid = true,
                Year = 2024,
                MonthNumber = 2,
                Electricity = emptyUtilities2.Elec,
                ColdWater = emptyUtilities2.Water,
                HotWater = emptyUtilities2.Hot,
                NaturalGas = emptyUtilities2.Gas,
                Steam = emptyUtilities2.Steam,
                CentralHeating = emptyUtilities2.Heat
            };

            _context.Months.AddRange(m1, m2);
            _context.SaveChanges();

            _controller = new SummaryController(_context);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] 
            {
                new Claim(ClaimTypes.NameIdentifier, "u1")
            }, "mock"));
            _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };
        }

        [TearDown]
        public void TearDown()
        {
            if (_controller is IDisposable disposableController)
            {
                disposableController.Dispose();
            }

            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void AverageUsage_Calculates_Correct_Values()
        {
            var model = new Monthly_Bill_Calculator.Models.SummaryViewModel
            {
                StartYear = 2024,
                StartMonth = 1,
                EndYear = 2024,
                EndMonth = 2
            };

            var result = _controller.AverageUsage(model) as ViewResult;
            var resultModel = result.Model as Monthly_Bill_Calculator.Models.SummaryViewModel;

            Assert.AreEqual(150, resultModel.Utilities["Electricity"].AvgConsumption);
        }
    }
}