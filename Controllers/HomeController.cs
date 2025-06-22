using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalBudgetTackerMVCProject.Data;
using PersonalBudgetTackerMVCProject.Models;
using System.Linq;

namespace PersonalBudgetTackerMVCProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var userName = User.Identity?.Name;

            var income = _context.Transaction
                .Where(t => t.UserId == userId && t.Category.Type == "Income")
                .Sum(t => (decimal?)t.Amount) ?? 0;

            var expense = _context.Transaction
                .Where(t => t.UserId == userId && t.Category.Type == "Expense")
                .Sum(t => (decimal?)t.Amount) ?? 0;

            var viewModel = new HomeViewModel
            {
                UserName = userName,
                TotalIncome = income,
                TotalExpense = expense
            };

            return View(viewModel);
        }
    }
}
