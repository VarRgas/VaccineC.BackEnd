namespace VaccineC.Query.Model.Models
{
    public class ProductSummaryBatch
    {
        public Guid ID { get; set; }
        public string Batch { get; set; }
        public decimal NumberOfUnitsBatch { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ValidityBatchDate { get; set; }
        public DateTime Register { get; set; }
        public string Manufacturer { get; set; }
        public Guid ProductsId { get; set; }
        public Product? Products { get; set; }
    }
}
