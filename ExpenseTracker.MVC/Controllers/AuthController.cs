using ExpenseTracker.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace ExpenseTracker.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // REGISTER PAGE
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var client = _httpClientFactory.CreateClient("ExpenseAPI");
            var response = await client.PostAsJsonAsync("api/auth/register", user);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login");

            ViewBag.Error = "Email already exists!";
            return View();
        }

        // LOGIN PAGE
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
public async Task<IActionResult> Login(string email, string password)
{
    var client = _httpClientFactory.CreateClient("ExpenseAPI");

    var response = await client.PostAsync(
        $"api/auth/login?email={email}&password={password}", null);

    if (response.IsSuccessStatusCode)
    {
        // Read raw JSON string
        var json = await response.Content.ReadAsStringAsync();

        // Deserialize using JsonDocument
        using var doc = System.Text.Json.JsonDocument.Parse(json);
        var root = doc.RootElement;

        string name = root.GetProperty("name").GetString();
        string userEmail = root.GetProperty("email").GetString();

        // Store in session
        HttpContext.Session.SetString("LoggedIn", "true");
        HttpContext.Session.SetString("Name", name);
        HttpContext.Session.SetString("Email", userEmail);

        return RedirectToAction("Dashboard", "Expenses");

    }

    ViewBag.Error = "Invalid email or password!";
    return View();
}


        // LOGOUT
        public IActionResult Logout()
{
    HttpContext.Session.Clear();   // Remove all session data
    return RedirectToAction("Login", "Auth");
}

    }
}
