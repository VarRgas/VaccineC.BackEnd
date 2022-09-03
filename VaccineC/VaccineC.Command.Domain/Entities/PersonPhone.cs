using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class PersonPhone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("personId")]
        public Guid PersonID { get; set; }

        [Column("phoneType", TypeName = "varchar(1)")]
        public string PhoneType { get; set; }

        [Column("numberPhone", TypeName = "varchar(20)")]
        public string NumberPhone { get; set; }

        [Column("codeArea", TypeName = "varchar(2)")]
        public string CodeArea { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public PersonPhone(Guid id, Guid personId, string phoneType, string numberPhone, string codeArea, DateTime register)
        {
            ID = id;
            PersonID = personId;    
            PhoneType = phoneType;
            NumberPhone = numberPhone;
            CodeArea = codeArea;
            Register = register;
        }
        public PersonPhone()
        {

        }

        public void SetPersonId(Guid personId)
        {
            PersonID = personId;
        }

        public void SetPhoneType(string phoneType)
        {
            PhoneType = phoneType;
        }

        public void SetNumberPhone(string numberPhone)
        {
            NumberPhone = numberPhone;
        }
        public void SetCodeArea(string codeArea)
        {
            CodeArea = codeArea;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
