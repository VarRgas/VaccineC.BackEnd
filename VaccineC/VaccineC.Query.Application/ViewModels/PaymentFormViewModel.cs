namespace VaccineC.Query.Application.ViewModels
{
    public class PaymentFormViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int MaximumInstallments { get; set; }
        public DateTime Register { get; set; }
    }
}
