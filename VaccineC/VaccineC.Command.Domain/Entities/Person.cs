using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class Person
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("personType", TypeName = "varchar(1)")]
        public string PersonType { get; set; }

        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Column("commemorativeDate", TypeName = "datetime")]
        public DateTime? CommemorativeDate { get; set; }

        [Column("email", TypeName = "varchar(255)")]
        public string? Email { get; set; }

        [Column("profilePic", TypeName = "varchar(255)")]
        public string? ProfilePic { get; set; }

        [Column("details", TypeName = "text")]
        public string? Details { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }


        public Person(Guid id, string personType, string name, DateTime commemorativeDate, string email, string profilePic, string details, DateTime register)
        {
            ID = id;
            PersonType = personType;
            Name = name;
            CommemorativeDate = commemorativeDate;
            Email = email;
            ProfilePic = profilePic;
            Details = details;
            Register = register;
        }
        public Person()
        {

        }
        public void SetName(string name)
        {
            Name = name;
        }

        public void SetPersonType(string personType)
        {
            PersonType = personType;
        }

        public void SetCommemorativeDate(DateTime commemorativeDate)
        {
            CommemorativeDate = commemorativeDate;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetProfilePic(string profilePic)
        {
            ProfilePic = profilePic;
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
