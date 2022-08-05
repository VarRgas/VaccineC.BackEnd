using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Query.Model.Models
{
    public class Example
    {

        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Phone { get; set; }

        [Column(TypeName = "varchar(11)")]
        public string CPF { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        public bool HasPending { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPending { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
