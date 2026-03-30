using Microsoft.AspNetCore.Identity;
using Monthly_Bill_Calculator.DB_Models;

namespace Monthly_Bill_Calculator.Data.SeedData
{
    public static class SeedAdminRole
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<CalcAppUser>>();

            string[] roles = { "Admin", "User" };

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            string adminUsername = "SuperAdmin";
            string adminPassword = "SuperSecret";

            CalcAppUser admin = await userManager.FindByNameAsync(adminUsername);

            if (admin == null)
            {
                admin = new CalcAppUser
                {
                    UserName = adminUsername,
                };

                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}