namespace VaccineC.Query.Model.Models
{
    public class ProductDoses
    {
        public Guid ID { get; set; }
        public Guid ProductsId { get; set; }
        public string DoseType { get; set; }
        public DateTime Register { get; set; }
        public int? DoseRangeMonth { get; set; }
    }
}
