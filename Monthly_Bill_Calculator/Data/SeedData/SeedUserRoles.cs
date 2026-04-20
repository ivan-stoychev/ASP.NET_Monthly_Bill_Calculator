using Microsoft.AspNetCore.Identity;
using Monthly_Bill_Calculator.DB_Models;

namespace Monthly_Bill_Calculator.Data.SeedData
{
    public static class SeedUserRoles
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<CalcAppUser> userManager = serviceProvider.GetRequiredService<UserManager<CalcAppUser>>();

            string[] roles = { "Admin", "User", "Viewer" };

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}