using Microsoft.EntityFrameworkCore;
using Monthly_Bill_Calculator.Data;

namespace Monthly_Bill_Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Register DbContext in DI container
            builder.Services.AddDbContext<CalcAppDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            // Add services to the container
            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

            // Ensure database exists on startup
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CalcAppDbContext>();
                db.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}