using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class Example
    {
        public int Id { get; private set; }

        [Column(TypeName = "varchar(100)")]
        public string Name { get; private set; }

        [Column(TypeName = "varchar(50)")]
        public string Phone { get; private set; }

        [Column(TypeName = "varchar(11)")]
        public string CPF { get; private set; }

        [Column(TypeName = "varchar(100)")]
        public string Email { get; private set; }

        public bool HasPending { get; private set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPending { get; private set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public Example(string name, string phone, string cPF, string email)
        {
            Name = name;
            Phone = phone;
            CPF = cPF;
            Email = email;
        }

        public Example()
        {

        }

        public void SetName(string name)
        {
            Name = name;
        }
        public void SetPhone(string phone)
        {
            Phone = phone;
        }
        public void SetCPF(string cPF)
        {
            CPF = cPF;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }
        public void SetHasPending(bool hasPending)
        {
            HasPending = hasPending;
        }
        public void SetAmountPending(decimal amountPending)
        {
            AmountPending = amountPending;
        }
    }
}
