using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column("id")]
        public Guid ID { get; set; }

        [Column("sbimVaccinesId")]
        public Guid? SbimVaccinesId { get; set; }

        [Column("situation", TypeName = "varchar(1)")]
        public string Situation { get; set; }

        [Column("details", TypeName = "text")]
        public string? Details { get; set; }

        [Column("saleValue", TypeName = "decimal")]
        public decimal SaleValue { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Column("minimumStock", TypeName = "int")]
        public int MinimumStock { get; set; }



        public Product(Guid id, Guid? sbimVaccinesId, string situation, string? details, decimal saleValue, DateTime register, string name, int minimumStock)
        {
            ID = id;
            SbimVaccinesId = sbimVaccinesId;
            Situation = situation;
            Details = details;
            SaleValue = saleValue;
            Register = register;
            Name = name;
            MinimumStock = minimumStock;
        }
        public Product()
        {

        }

        public void SetSbimVaccinesId(Guid? sbimVaccinesId)
        {
            SbimVaccinesId = sbimVaccinesId;
        }

        public void SetSituation(string situation)
        {
            Situation = situation;
        }

        public void SetDetails(string? details)
        {
            Details = details;
        }

        public void SetSaleValue(decimal saleValue)
        {
            SaleValue = saleValue;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetMinimumStock(int minimumStock)
        {
            MinimumStock = minimumStock;
        }
    }
}