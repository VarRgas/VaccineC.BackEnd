using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("personID")]
        public Guid PersonId { get; set; }

        [Column("email", TypeName = "varchar(255)")]
        public string Email { get; set; }

        [Column("password", TypeName = "varchar(255)")]
        public string Password { get; set; }

        [Column("situation", TypeName = "varchar(1)")]
        public string Situation { get; set; }

        [Column("functionUser", TypeName = "varchar(1)")]
        public string FunctionUser { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public User(Guid id, Guid personId, string email, string password, string situation, string functionUser, DateTime register)
        {
            ID = id;
            PersonId = personId;
            Email = email;
            Password = password;
            Situation = situation;
            FunctionUser = functionUser;
            Register = register;
        }

        public User()
        {

        }

        public void SetPersonId(Guid personId)
        {
            PersonId = personId;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void SetSituation(string situation)
        {
            Situation = situation;
        }

        public void SetFunctionUser(string functionUser)
        {
            FunctionUser = functionUser;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
