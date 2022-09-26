namespace VaccineC.Query.Application.ViewModels
{
    public class BudgetProductViewModel
    {
        public Guid ID { get; set; }
        public Guid BudgetId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? BorrowerPersonId { get; set; }
        public string? ProductDose { get; set; }
        public string? Details { get; set; }
        public decimal EstimatedSalesValue { get; set; }
        public string SituationProduct { get; set; }
        public DateTime Register { get; set; }
        public ProductViewModel? Product { get; set; }
        public PersonViewModel? Person { get; set; }
    }
}
