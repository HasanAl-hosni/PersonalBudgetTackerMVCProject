using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalBudgetTackerMVCProject.Data;
using System.Linq;

namespace PersonalBudgetTackerMVCProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SummaryApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SummaryApiController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetSummary()
        {
            var userId = _userManager.GetUserId(User);

            var income = _context.Transaction
                .Where(t => t.UserId == userId && t.Category.Type == "Income")
                .Sum(t => (decimal?)t.Amount) ?? 0;

            var expense = _context.Transaction
                .Where(t => t.UserId == userId && t.Category.Type == "Expense")
                .Sum(t => (decimal?)t.Amount) ?? 0;

            var balance = income - expense;

            return Ok(new
            {
                income,
                expense,
                balance
            });
        }
    }
}
