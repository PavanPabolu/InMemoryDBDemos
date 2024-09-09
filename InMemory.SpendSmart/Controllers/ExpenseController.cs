using InMemory.SpendSmart.Data;
using InMemory.SpendSmart.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InMemory.SpendSmart.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly SpendSmartDbContext _context;

        public ExpenseController(ILogger<ExpenseController> logger, SpendSmartDbContext context)
        {
            _logger = logger;
            this._context = context;
        }

        public IActionResult Index()
        {
            var expenses = _context.Expenses.ToList();

            var totalExpenses = expenses.Sum(s => s.Value);

            ViewBag.TotalExpenses = totalExpenses;

            return View(expenses);
        }

        [Route("create-edit-an-expense")]
        public IActionResult CreateEditExpense(int? id)
        {
            //var model = new Expense();

            if (id is null || id <= 0)
            {
                ViewData["SectionTitle"] = "Add Expense";
                return View(new Expense());
            }
            else
            {
                ViewData["SectionTitle"] = "Edit Expense";
                var model = _context.Expenses.SingleOrDefault(e => e.Id == id);
                return View(model);
            }

            return View();
        }

        //[Route("create-edit-an-expense")]
        [HttpPost("create-edit-an-expense")]
        //[ValidateAntiForgeryToken]
        public IActionResult CreateEditExpense(Expense model)
        {
            //if (!ModelState.IsValid) { }

            if (model.Id == 0)
                _context.Expenses.Add(model);
            else
                _context.Expenses.Update(model);
            _context.SaveChanges();

            ViewBag.FormMessage = "Added Successfully!";

            return RedirectToAction("Index");
        }

        public IActionResult DeleteExpense(int id)
        {
            if (id > 0)
            {
                var model = _context.Expenses.SingleOrDefault(e => e.Id == id);
                if (model != null)
                {
                    _context.Expenses.Remove(model);
                    _context.SaveChanges();

                    ViewBag.Message = "Deleted Successfully!";
                }
                else
                    ViewBag.Message = "Deletion failed!- Record not found in data store.";
            }
            else
                ViewBag.Message = "Deletion failed!";

            return RedirectToAction("Index");
        }
    }
}
