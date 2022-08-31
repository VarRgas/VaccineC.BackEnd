using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class PaymentForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Column("maximumInstallments", TypeName = "int")]
        public int MaximumInstallments { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public PaymentForm(Guid id, string name, int maximumInstallments, DateTime register)
        {
            ID = id;
            Name = name;
            MaximumInstallments = maximumInstallments;
            Register = register;
        }

        public PaymentForm()
        {
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetMaximumInstallments(int maximumInstallments)
        {
            MaximumInstallments = maximumInstallments;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }

    }
}
