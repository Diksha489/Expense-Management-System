using ExpenseTracker.API.Data;
using ExpenseTracker.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            // Check if user already exists
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return BadRequest("Email already exists");

            // Hash password
            user.PasswordHash = HashPassword(user.PasswordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        // LOGIN
        [HttpPost("login")]
public async Task<IActionResult> Login(string email, string password)
{
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    if (user == null)
        return Unauthorized("Invalid email");

    if (user.PasswordHash != HashPassword(password))
        return Unauthorized("Invalid password");

    return Ok(new { user.Id, user.Name, user.Email });
}


        // PASSWORD HASHING
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }
    }
}
