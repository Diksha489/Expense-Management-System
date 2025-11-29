using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.API.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Category { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
