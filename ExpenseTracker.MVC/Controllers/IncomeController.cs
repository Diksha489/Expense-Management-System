using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.MVC.Data;
using ExpenseTracker.MVC.Models;

public class IncomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public IncomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var incomes = _context.Incomes.ToList();
        return View(incomes);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Income income)
    {
        if (ModelState.IsValid)
        {
            _context.Incomes.Add(income);
            _context.SaveChanges();
            TempData["success"] = "Income added successfully!";
            return RedirectToAction("Index");
        }
        return View(income);
    }

    public IActionResult Edit(int id)
    {
        var income = _context.Incomes.Find(id);
        return View(income);
    }

    [HttpPost]
    public IActionResult Edit(Income income)
    {
        _context.Incomes.Update(income);
        _context.SaveChanges();
        TempData["success"] = "Income updated!";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var income = _context.Incomes.Find(id);
        return View(income);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var income = _context.Incomes.Find(id);
        _context.Incomes.Remove(income);
        _context.SaveChanges();
        TempData["success"] = "Income deleted!";
        return RedirectToAction("Index");
    }
}
