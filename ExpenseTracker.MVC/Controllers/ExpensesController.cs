using Newtonsoft.Json;
using ExpenseTracker.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using ExpenseTracker.MVC.Data;

namespace ExpenseTracker.MVC.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApplicationDbContext _context; 

        public ExpensesController(IHttpClientFactory httpClientFactory, ApplicationDbContext context)
        {
            _httpClientFactory = httpClientFactory;
             _context = context;
        }

        // GET: /Expenses
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("LoggedIn") != "true")
    return RedirectToAction("Login", "Auth");

            var client = _httpClientFactory.CreateClient("ExpenseAPI");
            var expenses = await client.GetFromJsonAsync<List<Expense>>("api/expenses");

            return View(expenses);
        }

        // GET: /Expenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Expenses/Create
        [HttpPost]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (HttpContext.Session.GetString("LoggedIn") != "true")
    return RedirectToAction("Login", "Auth");

            var client = _httpClientFactory.CreateClient("ExpenseAPI");
            var response = await client.PostAsJsonAsync("api/expenses", expense);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(expense);
        }

        // GET: /Expenses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("LoggedIn") != "true")
    return RedirectToAction("Login", "Auth");

            var client = _httpClientFactory.CreateClient("ExpenseAPI");

            var expense = await client.GetFromJsonAsync<Expense>($"api/expenses/{id}");

            if (expense == null)
                return NotFound();

            return View(expense);
        }

        // POST: /Expenses/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Expense expense)
        {
            if (HttpContext.Session.GetString("LoggedIn") != "true")
    return RedirectToAction("Login", "Auth");

            var client = _httpClientFactory.CreateClient("ExpenseAPI");
            var response = await client.PutAsJsonAsync($"api/expenses/{expense.Id}", expense);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(expense);
        }

        // GET: /Expenses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("LoggedIn") != "true")
    return RedirectToAction("Login", "Auth");

            var client = _httpClientFactory.CreateClient("ExpenseAPI");
            var expense = await client.GetFromJsonAsync<Expense>($"api/expenses/{id}");

            if (expense == null)
                return NotFound();

            return View(expense);
        }

        // POST: /Expenses/DeleteConfirmed
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("ExpenseAPI");
            var response = await client.DeleteAsync($"api/expenses/{id}");

            return RedirectToAction("Index");
        }

      public IActionResult Dashboard()
{
    var expenses = _context.Expenses.ToList();
    var incomes = _context.Incomes.ToList();

    // Expense summaries
    ViewBag.TodayTotal = expenses.Where(x => x.Date.Date == DateTime.Now.Date).Sum(x => x.Amount);
    ViewBag.YesterdayTotal = expenses.Where(x => x.Date.Date == DateTime.Now.AddDays(-1).Date).Sum(x => x.Amount);
    ViewBag.ThisWeekTotal = expenses.Where(x => x.Date >= DateTime.Now.AddDays(-7)).Sum(x => x.Amount);
    ViewBag.ThisMonthTotal = expenses.Where(x => x.Date.Month == DateTime.Now.Month).Sum(x => x.Amount);

    // Income summary
    ViewBag.TotalIncome = incomes.Sum(x => x.Amount);
    ViewBag.TotalExpense = expenses.Sum(x => x.Amount);
    ViewBag.TotalSavings = ViewBag.TotalIncome - ViewBag.TotalExpense;

    // Category Pie Chart
    ViewBag.CategoryData = Newtonsoft.Json.JsonConvert.SerializeObject(
       expenses.GroupBy(x => x.Category)
               .Select(g => new { Category = g.Key, Amount = g.Sum(x => x.Amount) })
    );

    // Monthly Expense Chart
    ViewBag.MonthlyExpense = Newtonsoft.Json.JsonConvert.SerializeObject(
        expenses.GroupBy(e => e.Date.ToString("MMM"))
                .Select(g => new { Month = g.Key, Total = g.Sum(x => x.Amount) })
                .OrderBy(x => x.Month)
    );

    // Income vs Expense Chart
    ViewBag.IncomeExpenseCompare = Newtonsoft.Json.JsonConvert.SerializeObject(
        expenses.GroupBy(e => e.Date.ToString("MMM"))
        .Select(g => new
        {
            Month = g.Key,
            Expense = g.Sum(x => x.Amount),
            Income = incomes.Where(i => i.Date.ToString("MMM") == g.Key).Sum(i => i.Amount)
        }).OrderBy(x => x.Month)
    );

    return View();
}




    }
}
