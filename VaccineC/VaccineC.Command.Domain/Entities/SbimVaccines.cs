using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class SbimVaccines
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column("id")]
        public Guid ID { get; set; }

        [Column("name", TypeName = "varchar(1)")]
        public string Name { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }




        public SbimVaccines(Guid id, string name, DateTime register)
        {
            ID = id;
            Name = name;
            Register = register;
        }
        public SbimVaccines()
        {

        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
