using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PersonalBudgetTackerMVCProject.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Note { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        
        public string UserId { get; set; } = string.Empty;
        public IdentityUser? User { get; set; }  // Navigation property
    }

}
