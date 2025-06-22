using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBudgetTackerMVCProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        
    }
}
