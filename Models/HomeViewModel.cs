namespace PersonalBudgetTackerMVCProject.Models
{
    public class HomeViewModel
    {
        public string UserName { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance => TotalIncome - TotalExpense;
    }
}
