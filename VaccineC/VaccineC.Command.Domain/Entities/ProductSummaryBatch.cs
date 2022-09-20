using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class ProductSummaryBatch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("batch", TypeName = "varchar(20)")]
        public string Batch { get; set; }

        [Column("numberOfUnitsBatch", TypeName = "numeric(15,2)")]
        public decimal NumberOfUnitsBatch { get; set; }

        [Column("manufacturingDate", TypeName = "datetime")]
        public DateTime? ManufacturingDate { get; set; }

        [Column("validityBatchDate", TypeName = "datetime")]
        public DateTime ValidityBatchDate { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        [Column("manufacturer", TypeName = "varchar(255)")]
        public string Manufacturer { get; set; }

        [Column("productsId")]
        public Guid ProductsId { get; set; }

        public ProductSummaryBatch(Guid id, string batch, decimal numberOfUnitsBatch, DateTime? manufacturingDate, DateTime validityBatchDate, DateTime register, string manufacturer, Guid productsId)
        {
            ID = id;
            Batch = batch;
            NumberOfUnitsBatch = numberOfUnitsBatch;
            ManufacturingDate = manufacturingDate;
            ValidityBatchDate = validityBatchDate;
            Register = register;
            Manufacturer = manufacturer;
            ProductsId = productsId;
        }

        public ProductSummaryBatch()
        {
        }

        public void SetBatch(string batch)
        {
            Batch = batch;
        }

        public void SetNumberOfUnitsBatch(decimal numberOfUnitsBatch)
        {
            NumberOfUnitsBatch = numberOfUnitsBatch;
        }

        public void SetManufacturingDate(DateTime? manufacturingDate)
        {
            ManufacturingDate = manufacturingDate;
        }

        public void SetValidityBatchDate(DateTime validityBatchDate)
        {
            ValidityBatchDate = validityBatchDate;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }

        public void SetManufacturer(string manufacturer)
        {
            Manufacturer = manufacturer;
        }

        public void SetProductsId(Guid productsId)
        {
            ProductsId = productsId;
        }
    }
}
