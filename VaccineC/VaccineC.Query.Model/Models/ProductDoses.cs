namespace VaccineC.Query.Model.Models
{
    public class ProductDoses
    {
        public Guid ID { get; set; }
        public Guid ProductId { get; set; }
        public string DoseType { get; set; }
        public DateTime Register { get; set; }
        public int? DoseRangeMonth { get; set; }
    }
}
