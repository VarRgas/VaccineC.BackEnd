using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class ProductDoses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column("id")]
        public Guid ID { get; set; }

        [Column("productsDosesId")]
        public Guid ProductsId { get; set; }

        [Column("doseType", TypeName = "varchar(2)")]
        public string DoseType { get; set; }

        [Column("doseRangeMonth", TypeName = "int")]
        public int? DoseRangeMonth { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public ProductDoses(Guid id, Guid productsId, string doseType, int? doseRangeMonth, DateTime register)
        {
            ID = id;
            ProductsId = productsId;
            DoseType = doseType;
            DoseRangeMonth = doseRangeMonth;
            Register = register;
        }
        public ProductDoses()
        {

        }

        public void SetProductsId(Guid productsId)
        {
            ProductsId = productsId;
        }

        public void SetDoseType(string doseType)
        {
            DoseType = doseType;
        }

        public void SetDoseRangeMonth(int? doseRangeMonth)
        {
            DoseRangeMonth = doseRangeMonth;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}