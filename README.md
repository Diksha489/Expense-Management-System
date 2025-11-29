📌 Expense Management System  
A complete Expense Management System built using ASP.NET MVC, ASP.NET Web API, C#, and SQL Server.
---

This project contains two separate applications:  
ExpenseManagement.API → Handles all data operations through REST APIs  
ExpenseManagement.MVC → User interface to manage expenses with dashboard, forms, and charts  

✨ Features  
🧾 Expense Management  
Add, edit, and delete expenses  
Category-wise tracking (Food, Travel, Shopping, etc.)  
Icon-based category display  
Filter by date and category  
Monthly and yearly summaries  

📊 Dashboard & Analytics  
Pie chart: Category-wise distribution  
Bar chart: Monthly expenses  
Line chart: Savings/expense trend    
Total balance cards 
Clean and responsive UI  

🔗 API Features  
REST API built with ASP.NET Web API  
JSON responses for:  
Expenses  
Categories  
Dashboard totals  
Follows clean controller structure  
Uses Entity Framework for data access  

🛠️ Tech Stack  
Frontend	ASP.NET MVC (Razor Views), Bootstrap  
Backend	ASP.NET Web API, C#, .NET Framework / .NET Core  
ORM	Entity Framework  
Database	SQL Server  
Chart.js    
API Format	REST + JSON  

📁 Project Structure  
ExpenseManagementSystem/  
│── ExpenseManagement.API/       → Backend API project  
│── ExpenseManagement.MVC/       → MVC UI project  
│── ExpenseManagementSystem.sln  → Solution file  
│── README.md  

🚀 How to Run the Project  
1️⃣ Clone the Repository  
git clone https://github.com/YOUR_USERNAME/expense-management-system.git  

2️⃣ Open Solution  
Open ExpenseManagementSystem.sln in Visual Studio  
Both projects (.MVC + .API) will load automatically  

🗄️ Database Setup  
✔ Update Connection String  
In API → appsettings.json or Web.config:  
"ConnectionStrings": {  
  "DefaultConnection": "Server=YOUR_SERVER;Database=ExpenseDB;Trusted_Connection=True;"  
}  

✔ Apply Migrations (if using EF)   
Run in Package Manager Console:  
Update-Database  
OR import the .sql file if included.  
▶️ Run the Applications  

🟦 Run API  
Set ExpenseManagement.API as Startup Project  
API will start at:  
https://localhost:PORT/api/...  

🟩 Run MVC App  
Set ExpenseManagement.MVC as Startup Project  
MVC will fetch all data from the API  
