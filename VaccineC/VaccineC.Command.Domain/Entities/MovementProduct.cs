using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class MovementProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("movementId")]
        public Guid MovementId { get; set; }

        [Column("productId")]
        public Guid ProductId { get; set; }

        [Column("batch", TypeName = "varchar(10)")]
        public string? Batch { get; set; }

        [Column("unitsNumber", TypeName = "numeric(15,2)")]
        public decimal UnitsNumber { get; set; }

        [Column("unitaryValue", TypeName = "numeric(15,2)")]
        public decimal UnitaryValue { get; set; }

        [Column("amount", TypeName = "numeric(15,2)")]
        public decimal Amount { get; set; }

        [Column("details", TypeName = "text")]
        public string? Details { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        [Column("batchManufacturingDate", TypeName = "datetime")]
        public DateTime? BatchManufacturingDate { get; set; }

        [Column("batchExpirationDate", TypeName = "datetime")]
        public DateTime? BatchExpirationDate { get; set; }

        [Column("manufacturer", TypeName = "varchar(255)")]
        public string? Manufacturer { get; set; }
 
        public MovementProduct(Guid id, Guid movementId, Guid productId, string? batch, decimal unitsNumber, decimal unitaryValue, decimal amount, string? details, DateTime register, DateTime? batchManufacturingDate, DateTime? batchExpirationDate, string? manufacturer)
        {
            ID = id;
            MovementId = movementId;
            ProductId = productId;
            Batch = batch;
            UnitsNumber = unitsNumber;
            UnitaryValue = unitaryValue;
            Amount = amount;
            Details = details;
            Register = register;
            BatchManufacturingDate = batchManufacturingDate;
            BatchExpirationDate = batchExpirationDate;
            Manufacturer = manufacturer;
        }

        public MovementProduct() { 
        }

        public void SetMovementId(Guid movementId)
        {
            MovementId = movementId;
        }

        public void SetProductId(Guid productId)
        {
            ProductId = productId;
        }

        public void SetBatch(string batch)
        {
            Batch = batch;
        }

        public void SetUnitsNumber(decimal unitsNumber)
        {
            UnitsNumber = unitsNumber;
        }

        public void SetUnitaryValue(decimal unitaryValue)
        {
            UnitaryValue = unitaryValue;
        }

        public void SetAmount(decimal amount)
        {
            Amount = amount;
        }

        public void SetDetails(string details)
        {
            Details = details;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }

        public void SetBatchManufacturingDate(DateTime? batchManufacturingDate)
        {
            BatchManufacturingDate = batchManufacturingDate;
        }

        public void SetBatchExpirationDate(DateTime? batchExpirationDate)
        {
            BatchExpirationDate = batchExpirationDate;
        }

        public void SetManufacturer(string manufacturer)
        {
            Manufacturer = manufacturer;
        }

    }
}
