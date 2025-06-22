using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalBudgetTackerMVCProject.Models;

namespace PersonalBudgetTackerMVCProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PersonalBudgetTackerMVCProject.Models.Transaction> Transaction { get; set; } = default!;
        public DbSet<PersonalBudgetTackerMVCProject.Models.Category> Category { get; set; } = default!;
    }
}
