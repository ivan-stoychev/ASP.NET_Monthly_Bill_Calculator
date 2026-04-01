using Microsoft.AspNetCore.Identity;
using Monthly_Bill_Calculator.DB_Models;

namespace Monthly_Bill_Calculator.Data.SeedData
{
    public static class SeedAdminRole
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<CalcAppUser> userManager = serviceProvider.GetRequiredService<UserManager<CalcAppUser>>();

            string[] roles = { "Admin", "User" };

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

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
                    // Optional: log errors for debugging
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Seeding error: {error.Description}");
                    }
                }
            }
        }
    }
}



//// TEST CODE FOR SEEDING ADMIN ROLE - VALID PASSWORD


//using Microsoft.AspNetCore.Identity;
//using Monthly_Bill_Calculator.DB_Models;

//namespace Monthly_Bill_Calculator.Data.SeedData
//{
//    public static class SeedAdminRole
//    {
//        public static async Task SeedAsync(IServiceProvider serviceProvider)
//        {
//            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//            UserManager<CalcAppUser> userManager = serviceProvider.GetRequiredService<UserManager<CalcAppUser>>();

//            string[] roles = { "Admin", "User" };

//            foreach (string role in roles)
//            {
//                if (!await roleManager.RoleExistsAsync(role))
//                    await roleManager.CreateAsync(new IdentityRole(role));
//            }

//            string adminEmail = "superadmin@example.com";
//            string adminPassword = "SuperSecret123!"; // VALID PASSWORD

//            CalcAppUser admin = await userManager.FindByEmailAsync(adminEmail);

//            if (admin == null)
//            {
//                admin = new CalcAppUser
//                {
//                    UserName = adminEmail,
//                    Email = adminEmail,
//                    EmailConfirmed = true
//                };

//                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);

//                if (result.Succeeded)
//                {
//                    await userManager.AddToRoleAsync(admin, "Admin");
//                    Console.WriteLine("ADMIN USER CREATED SUCCESSFULLY");
//                }
//                else
//                {
//                    Console.WriteLine("ADMIN USER CREATION FAILED:");
//                    foreach (var error in result.Errors)
//                    {
//                        Console.WriteLine($" - {error.Code}: {error.Description}");
//                    }
//                }
//            }
//            else
//            {
//                Console.WriteLine("ADMIN USER ALREADY EXISTS");
//            }
//        }
//    }
//}