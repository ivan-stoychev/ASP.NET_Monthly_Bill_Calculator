# Monthly Bill Calculator

A simple and clean ASP.NET Core MVC application for tracking monthly utility expenses.  
The goal of this project is to make it easy to record, view, and manage monthly bills such as electricity, water, gas, heating, and more — all stored locally for full privacy.

---

## 🚀 Features

- Add, edit, and delete monthly bill entries  
- Track multiple utilities per month (electricity, water, gas, steam, heating)  
- Automatic sorting by year and month  
- Clean and simple UI  
- No external data collection — everything stays on your device  

---

## 📂 Technologies Used

- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server / LocalDB**
- **Bootstrap 5**
- **C#**

- ## On first start, the user must create the database.
- Use command in package manager console:
- Add-Migration InitialCreate
- after that:
- Update-Database
- A database with the name "MonthlyBillCalculator" will be created.
- It has initial seed data for testing the application.
- 
git clone https://github.com/your-username/Monthly-Bill-Calculator.git
