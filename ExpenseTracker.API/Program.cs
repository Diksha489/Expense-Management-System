using ExpenseTracker.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Services for Controllers (MVC)
builder.Services.AddControllers();

// Add new OpenAPI (Swagger replacement for .NET 10)
builder.Services.AddOpenApi();

var app = builder.Build();

// Enable OpenAPI (Swagger UI)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();   // Enable controllers

app.Run();
