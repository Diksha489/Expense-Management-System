using Microsoft.EntityFrameworkCore;
using ExpenseTracker.API.Models;

namespace ExpenseTracker.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }
    
    


    }
}
