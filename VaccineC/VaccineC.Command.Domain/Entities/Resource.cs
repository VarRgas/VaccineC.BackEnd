using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Column("urlName", TypeName = "varchar(255)")]
        public string UrlName { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public Resource(Guid id, string name, string urlName, DateTime register)
        {
            ID = id;
            Name = name;
            UrlName = urlName;
            Register = register;
        }
        public Resource()
        {

        }
        public void SetName(string name)
        {
            Name = name;
        }

        public void SetUrlName(string urlName)
        {
            UrlName = urlName;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
