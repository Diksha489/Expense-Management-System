using Microsoft.EntityFrameworkCore;
using ExpenseTracker.MVC.Models;

namespace ExpenseTracker.MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Income> Incomes { get; set; }
    


    }
}
