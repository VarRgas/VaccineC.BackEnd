using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class BudgetHistoric
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("budgetId")]
        public Guid BudgetId { get; set; }

        [Column("userId")]
        public Guid? UserId { get; set; }

        [Column("historic", TypeName = "varchar(255)")]
        public string Historic { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public BudgetHistoric(Guid id, Guid budgetId, Guid? userId, string historic, DateTime register)
        {
            ID = id;
            BudgetId = budgetId;
            UserId = userId;
            Historic = historic;
            Register = register;
        }

        public BudgetHistoric()
        {

        }

        public void SetBudgetId(Guid budgetId)
        {
            BudgetId = budgetId;
        }

        public void SetUserId(Guid? userId)
        {
            UserId = userId;
        }
        public void SetHistoric(string historic)
        {
            Historic = historic;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
