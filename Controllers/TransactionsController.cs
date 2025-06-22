using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalBudgetTackerMVCProject.Data;
using PersonalBudgetTackerMVCProject.Models;

namespace PersonalBudgetTackerMVCProject.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TransactionsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {

            var userId = _userManager.GetUserId(User);
            var transactions = await _context.Transaction
                .Where(t => t.UserId == userId)
                .Include(t => t.Category)
                .ToListAsync();

            return View(transactions);
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var transaction = await _context.Transaction
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);
            if (transaction.UserId != userId)
                return Unauthorized();

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Title");
            return View();
        }

        // POST: Transactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Date,Note,CategoryId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                transaction.UserId = _userManager.GetUserId(User);
                _context.Add(transaction);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Title", transaction.CategoryId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);
            if (transaction.UserId != userId)
                return Unauthorized();

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Title", transaction.CategoryId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,Date,Note,CategoryId")] Transaction transaction)
        {
            if (id != transaction.Id)
                return NotFound();

            var userId = _userManager.GetUserId(User);
            var existingTransaction = await _context.Transaction.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (existingTransaction == null)
                return Unauthorized();

            if (ModelState.IsValid)
            {
                try
                {
                    transaction.UserId = userId; // assign user ID server-side
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Title", transaction.CategoryId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var transaction = await _context.Transaction
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);
            if (transaction.UserId != userId)
                return Unauthorized();

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var transaction = await _context.Transaction.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (transaction == null)
                return Unauthorized();

            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transaction.Any(e => e.Id == id);
        }
    }
}
