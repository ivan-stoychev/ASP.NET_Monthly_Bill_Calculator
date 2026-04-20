using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.Areas.Admin.Controllers;
using Monthly_Bill_Calculator.Data;
using Monthly_Bill_Calculator.DB_Models;
using NUnit.Framework;

namespace Monthly_Bill_Calculator.Tests
{
    [TestFixture]
    public class AdminMonthControllerTests
    {
        private CalcAppDbContext _context;
        private AdminMonthController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CalcAppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new CalcAppDbContext(options);

            _context.Months.Add(new Month { Id = 1, Year = 2024, MonthNumber = 1, User = new CalcAppUser { Email = "test@test.com" } });
            _context.Months.Add(new Month { Id = 2, Year = 2023, MonthNumber = 5, User = new CalcAppUser { Email = "other@test.com" } });
            _context.SaveChanges();

            _controller = new AdminMonthController(_context);
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
        public void Index_Returns_Correct_Filtered_Data()
        {
            var result = _controller.Index(null, 2024, null, null, 1) as ViewResult;
            var model = result.Model as List<Month>;

            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(2024, model[0].Year);
        }

        [Test]
        public void Create_Post_Adds_Month_And_Redirects()
        {
            var existingUser = _context.Users.FirstOrDefault();

            var newMonth = new Month
            {
                Year = 2025,
                MonthNumber = 10,
                UserId = existingUser.Id
            };

            var result = _controller.Create(newMonth) as RedirectToActionResult;

            Assert.IsNotNull(result, "Контролерът не върна RedirectToActionResult");
            Assert.AreEqual("Index", result.ActionName);

            Assert.AreEqual(3, _context.Months.Count());
        }
    }
}