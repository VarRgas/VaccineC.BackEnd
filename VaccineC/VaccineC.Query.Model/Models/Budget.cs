namespace VaccineC.Query.Model.Models
{
    public class Budget
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
        public DateTime? CreationDate { get; set; }
        public string? Details { get; set; }
        public DateTime Register { get; set; }
        public int BudgetNumber { get; set; }
        public Person? Persons { get; set; }
        public User? Users { get; set; }
    }
}
