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

        [Column("rndsId", TypeName = "varchar(100)")]
        public string RndsId { get; set; }


        public SbimVaccines(Guid id, string name, DateTime register, string rndsId)
        {
            ID = id;
            Name = name;
            Register = register;
            RndsId = rndsId;
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

        public void SetRdnsId(string rdnsId)
        {
            RndsId = rdnsId;
        }
    }
}
