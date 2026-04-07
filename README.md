📘 Monthly Bill Calculator

A clean, modern ASP.NET Core MVC application for tracking monthly utility expenses.
The system allows users to record, view, and manage monthly bills such as electricity, water, gas, heating, and more — with full authentication, role-based access, and a dedicated admin area.
🚀 Features
👤 Authentication & Authorization

    ASP.NET Identity with User and Administrator roles

    Secure login, logout, and registration

    Admin-only area for managing all users’ bills

📅 Monthly Bill Management

    Add, view, and manage monthly utility entries

    Track multiple utilities per month:

        Electricity

        Cold Water

        Hot Water

        Natural Gas

        Steam

        Central Heating

    Automatic sorting by year and month

    Mark months as paid/unpaid

📊 Summary & Reporting

    Average usage summary

    Total usage summary

    Clean, readable tables

    Null-safe calculations

🧭 Navigation & UI

    Responsive Bootstrap 5 layout

    Clean navigation menu with role-based visibility

    Custom 404 error page

    Custom 500 error page (optional)

🗄 Database & Architecture

    SQL Server database (auto-created on first run)

    Entity Framework Core with migrations

    Seeded admin role + admin user

    MVC Areas (Admin area)

    Dependency Injection everywhere

🛠 Technologies Used

    ASP.NET Core MVC (.NET 8)

    Entity Framework Core

    SQL Server / LocalDB

    ASP.NET Identity

    Bootstrap 5

    C#

    Razor Views
    
📦 Installation & Setup
1. Clone the repository
bash

git clone https://github.com/your-username/Monthly-Bill-Calculator.git

2. Open the project

Use Visual Studio 2022 or JetBrains Rider.
3. Configure the database

The app uses the connection string in appsettings.json.
On first run, EF Core will automatically create the database:
Code

MonthlyBillCalculator

4. Run the application

Press F5 or run via CLI:
bash

dotnet run

5. Default Admin User

The system seeds an admin role and admin user automatically.
🧱 Project Structure
Code

/Areas
   /Admin
      /Controllers
      /Views
/Controllers
/Views
/Models
/Data
   CalcAppDbContext.cs
   SeedData

🧩 Entity Models (5+)

    Month

    Electricity

    ColdWater

    HotWater

    NaturalGas

    Steam

    CentralHeating

    CalcAppUser (Identity user)

🧭 Controllers (5+)

    HomeController

    MonthController

    SummaryController

    AdminMonthController (Admin Area)

    ErrorController

⚠️ Error Handling
Custom 404 Page

Displays a friendly message and guidance for login issues.
Custom 500 Page

(Recommended) Add a clean fallback for unexpected server errors.
🔐 Security

    ASP.NET Identity

    Anti-forgery tokens

    EF Core parameterized queries

    Razor auto-encoding (prevents XSS)

    Role-based authorization

    No raw SQL
