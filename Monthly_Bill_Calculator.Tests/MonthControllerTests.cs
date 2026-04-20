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
    public class MonthControllerTests
    {
        private CalcAppDbContext _context;
        private MonthController _controller;
        private string _userId = "user-123";

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CalcAppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new CalcAppDbContext(options);

            _context.Months.Add(new Month { Id = 1, UserId = _userId, IsPaid = false });
            _context.SaveChanges();

            _controller = new MonthController(_context);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
            new Claim(ClaimTypes.NameIdentifier, _userId)
        }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
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
        public void MarkAsPaid_Updates_Database()
        {
            _controller.MarkAsPaid(1);
            var month = _context.Months.Find(1);

            Assert.IsTrue(month.IsPaid);
        }
    }
}