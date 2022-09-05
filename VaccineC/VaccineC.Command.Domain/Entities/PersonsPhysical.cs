using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class PersonsPhysical
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("personId")]
        public Guid PersonId { get; set; }
        [Column("maritalStatus", TypeName = "varchar(1)")]
        public string MaritalStatus { get; set; }
        [Column("gender", TypeName = "varchar(1)")]
        public string Gender { get; set; }
        [Column("deathDate", TypeName = "date")]
        public DateTime? DeathDate { get; set; }
        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }
        [Column("cnsNumber", TypeName = "varchar(15)")]
        public string? CnsNumber { get; set; }
        [Column("cpfNumber", TypeName = "varchar(11)")]
        public string? CpfNumber { get; set; }


        public PersonsPhysical(Guid id, Guid personId, string maritalStatus, string gender, DateTime? deathDate, DateTime register, string? cnsNumber, string? cpfNumber)
        {
            ID = id;
            PersonId = personId;
            MaritalStatus = maritalStatus;
            Gender = gender;
            DeathDate = deathDate;
            Register = register;
            CnsNumber = cnsNumber;
            CpfNumber = cpfNumber;
        }
        public PersonsPhysical()
        {

        }

        public void SetMaritalStatus(string maritalStatus)
        {
            MaritalStatus = maritalStatus;
        }

        public void SetGender(string gender)
        {
            Gender = gender;
        }

        public void SetDeathDate(DateTime? deathDate)
        {
            DeathDate = deathDate;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }

        public void SetCNS(string? cns)
        {
            CnsNumber = cns;
        }

        public void SetCPF(string? cpf)
        {
            CpfNumber = cpf;
        }
    }
}
