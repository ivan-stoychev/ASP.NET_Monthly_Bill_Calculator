using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.DB_Models;

namespace Monthly_Bill_Calculator.Data.SeedData
{
    public static class SeedUsersData
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<CalcAppDbContext>();
            UserManager<CalcAppUser> userManager = serviceProvider.GetRequiredService<UserManager<CalcAppUser>>();

            if (!context.Months.Any())
            {
                var random = new Random();
                var allUsers = await userManager.Users.ToListAsync();
                var monthsData = new List<Month>();

                foreach (var user in allUsers)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        var newMonth = new Month
                        {
                            UserId = user.Id,
                            Year = 2024,
                            MonthNumber = i,
                            IsPaid = random.Next(0, 2) == 1,
                            Electricity = new Electricity { Consumption = random.Next(100, 500), Price = 0.22m },
                            ColdWater = new ColdWater { Consumption = random.Next(5, 20), Price = 1.50m },
                            HotWater = new HotWater { Consumption = random.Next(2, 10), Price = 4.50m },
                            NaturalGas = new NaturalGas { Consumption = 0, Price = 0 },
                            Steam = new Steam { Consumption = 0, Price = 0 },
                            CentralHeating = new CentralHeating { Consumption = random.Next(0, 5), Price = 90.00m }
                        };

                        monthsData.Add(newMonth);
                    }
                }

                await context.Months.AddRangeAsync(monthsData);
                await context.SaveChangesAsync();
            }
        }
    }
}