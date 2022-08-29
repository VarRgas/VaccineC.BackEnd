using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("personId")]
        public Guid PersonId { get; set; }

        [Column("details", TypeName = "varchar(255)")]
        public string Details { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public Company(Guid id, Guid personId, string details, DateTime register)
        {
            ID = id;
            PersonId = personId;
            Details = details;
            Register = register;
        }
        public Company()
        {

        }

        public void SetPersonId(Guid personId)
        {
            PersonId = personId;
        }

        public void SetDetails(string details)
        {
            Details = details;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
