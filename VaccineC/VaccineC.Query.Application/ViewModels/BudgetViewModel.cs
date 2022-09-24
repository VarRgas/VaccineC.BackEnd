namespace VaccineC.Query.Application.ViewModels
{
    public class BudgetViewModel
    {
        public Guid ID { get; set; }
        public Guid UserId { get; set; }
        public Guid PersonId { get; set; }
        public string Situation { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal TotalBudgetAmount { get; set; }
        public decimal TotalBudgetedAmount { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? Details { get; set; }
        public int BudgetNumber { get; set; }
        public DateTime Register { get; set; }
        public PersonViewModel? Persons { get; set; }
        public UserViewModel? Users { get; set; }
    }
}
