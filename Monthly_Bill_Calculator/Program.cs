using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.Data;
using Monthly_Bill_Calculator.DB_Models;
using Monthly_Bill_Calculator.Data.SeedData;

namespace Monthly_Bill_Calculator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Register DbContext
            builder.Services.AddDbContext<CalcAppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Identity + Roles
            builder.Services.AddDefaultIdentity<CalcAppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<CalcAppDbContext>();

            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

            // Ensure DB exists + Seed Roles/Admin
            using (IServiceScope scope = app.Services.CreateScope())
            {
                CalcAppDbContext db = scope.ServiceProvider.GetRequiredService<CalcAppDbContext>();
                db.Database.EnsureCreated();

                await SeedAdminRole.SeedAsync(scope.ServiceProvider);
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Area routing
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            // Default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}