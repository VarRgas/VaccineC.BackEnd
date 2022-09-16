namespace VaccineC.Query.Model.Models
{
    public class MovementProduct
    {
        public Guid ID { get; set; }
        public Guid MovementId { get; set; }
        public Guid ProductId { get; set; }
        public string? Batch { get; set; }
        public decimal UnitsNumber { get; set; }
        public decimal UnitaryValue { get; set; }
        public decimal Amount { get; set; }
        public string? Details { get; set; }
        public DateTime Register { get; set; }
        public DateTime? BatchManufacturingDate { get; set; }
        public DateTime? BatchExpirationDate { get; set; }
        public string? Manufacturer { get; set; }
        public Product? Product { get; set; }

    }
}
