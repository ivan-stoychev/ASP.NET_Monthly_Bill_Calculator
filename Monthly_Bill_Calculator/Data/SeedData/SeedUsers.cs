using Microsoft.AspNetCore.Identity;
using Monthly_Bill_Calculator.DB_Models;

namespace Monthly_Bill_Calculator.Data.SeedData
{
    public static class SeedUsers
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<CalcAppUser> userManager = serviceProvider.GetRequiredService<UserManager<CalcAppUser>>();

            // Seed admin user
            string adminEmail = "superadmin@example.com";
            string adminPassword = "SuperSecret!123";

            CalcAppUser admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new CalcAppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Seeding error: {error.Description}");
                    }
                }
            }

            // Seed regular users
            string[] userEmails = { "user1@example.com", "user2@example.com" };
            string[] userPasswords = { "Password1!", "Password2!" };

            foreach (var userEmail in userEmails)
            {
                CalcAppUser user = await userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    user = new CalcAppUser
                    {
                        UserName = userEmail,
                        Email = userEmail,
                        EmailConfirmed = true
                    };

                    IdentityResult result = await userManager.CreateAsync(user, userPasswords[Array.IndexOf(userEmails, userEmail)]);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "User");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Seeding error: {error.Description}");
                        }
                    }
                }
            }
        }
    }
}