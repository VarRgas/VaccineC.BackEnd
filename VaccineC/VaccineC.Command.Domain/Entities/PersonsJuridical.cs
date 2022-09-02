using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class PersonsJuridical
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("personId")]
        public Guid PersonId { get; set; }

        [Column("fantasyName", TypeName = "varchar(255)")]
        public string? FantasyName { get; set; }

        [Column("cnpjNumber", TypeName = "varchar(14)")]
        public string? CnpjNumber { get; set; }


        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }


        public PersonsJuridical(Guid id, Guid personId, string? fantasyName, string? cnpjNumber, DateTime register)
        {
            ID = id;
            PersonId = personId;
            FantasyName = fantasyName;
            CnpjNumber = cnpjNumber;
            Register = register;
        }
        public PersonsJuridical()
        {

        }
        public void SetFantasyName(string name)
        {
            FantasyName = name;
        }

        public void SetCnpjNumber(string cnpj)
        {
            CnpjNumber = cnpj;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
