using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class Movement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("movementNumber")]
        public int MovementNumber { get; private set; }

        [Column("usersId")]
        public Guid UsersId { get; set; }

        [Column("movementType", TypeName = "varchar(1)")]
        public string MovementType { get; set; }

        [Column("productsAmount", TypeName = "numeric(15,2)")]
        public decimal? ProductsAmount { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        [Column("situation", TypeName = "varchar(1)")]
        public string Situation { get; set; }

        public Movement(Guid id, Guid usersId, string movementType, decimal? productsAmount, DateTime register, string situation)
        {
            ID = id;
            UsersId = usersId;
            MovementType = movementType;
            ProductsAmount = productsAmount;
            Register = register;
            Situation = situation;
        }

        public Movement() { 
        }

        public void SetSituation(string situation)
        {
            Situation = situation;
        }

        public void SetProductsAmount(decimal? productsAmount)
        {
            ProductsAmount = productsAmount;
        }

        public void SetUsersId(Guid usersId)
        {
            UsersId = usersId;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
