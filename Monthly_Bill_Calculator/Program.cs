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

            builder.Services.AddDbContext<CalcAppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddDefaultIdentity<CalcAppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<CalcAppDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            WebApplication app = builder.Build();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                CalcAppDbContext db = scope.ServiceProvider.GetRequiredService<CalcAppDbContext>();

                db.Database.Migrate();

                await SeedAdminRole.SeedAsync(scope.ServiceProvider);
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error/500");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
