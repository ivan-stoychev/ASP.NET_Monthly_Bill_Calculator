using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.Data;
using Monthly_Bill_Calculator.Data.SeedData;
using Monthly_Bill_Calculator.DB_Models;
using System.Globalization;

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

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute());
            });
            builder.Services.AddRazorPages();

            // Fix culture issues with decimal separator in different locales
            var rootCulture = CultureInfo.InvariantCulture;
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(rootCulture),
                SupportedCultures = new List<CultureInfo> { rootCulture },
                SupportedUICultures = new List<CultureInfo> { rootCulture }
            };

            WebApplication app = builder.Build();

            app.UseRequestLocalization(localizationOptions);

            using (IServiceScope scope = app.Services.CreateScope())
            {
                CalcAppDbContext db = scope.ServiceProvider.GetRequiredService<CalcAppDbContext>();

                db.Database.Migrate();

                await SeedUserRoles.SeedAsync(scope.ServiceProvider);
                await SeedUsers.SeedAsync(scope.ServiceProvider);
                await SeedUsersData.SeedAsync(scope.ServiceProvider);
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
