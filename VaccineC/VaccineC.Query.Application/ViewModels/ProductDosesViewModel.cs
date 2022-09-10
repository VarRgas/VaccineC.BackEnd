namespace VaccineC.Query.Application.ViewModels
{
    public class ProductDosesViewModel
    {
        public Guid ID { get; set; }
        public Guid ProductsId { get; set; }
        public string DoseType { get; set; }
        public DateTime Register { get; set; }
        public int? DoseRangeMonth { get; set; }
    }
}
