using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class Budget
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("userId")]
        public Guid UserId { get; set; }

        [Column("personId")]
        public Guid PersonId { get; set; }

        [Column("situation", TypeName = "varchar(1)")]
        public string Situation { get; set; }

        [Column("discountPercentage", TypeName = "numeric(15,2)")]
        public decimal DiscountPercentage { get; set; }

        [Column("discountValue", TypeName = "numeric(15,2)")]
        public decimal DiscountValue { get; set; }

        [Column("totalBudgetAmount", TypeName = "numeric(15,2)")]
        public decimal TotalBudgetAmount { get; set; }

        [Column("totalBudgetedAmount", TypeName = "numeric(15,2)")]
        public decimal TotalBudgetedAmount { get; set; }

        [Column("expirationDate", TypeName = "datetime")]
        public DateTime? ExpirationDate { get; set; }

        [Column("creationDate", TypeName = "datetime")]
        public DateTime? CreationDate { get; set; }

        [Column("details", TypeName = "varchar(255)")]
        public string? Details { get; set; }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("budgetNumber")]
        public int BudgetNumber { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public Budget(Guid id, Guid userId, Guid personId, string situation, decimal discountPercentage, decimal discountValue,
            decimal totalBudgetAmount, decimal totalBudgetedAmount, DateTime? expirationDate, DateTime? creationDate, string? details, int budgetNumber, DateTime register)
        {
            ID = id;
            UserId = userId;
            PersonId = personId;
            Situation = situation;
            DiscountPercentage = discountPercentage;
            DiscountValue = discountValue;
            TotalBudgetAmount = totalBudgetAmount;
            TotalBudgetedAmount = totalBudgetedAmount;
            ExpirationDate = expirationDate;
            CreationDate = creationDate;
            Details = details;
            BudgetNumber = budgetNumber;
            Register = register;
        }
        public Budget()
        {

        }
        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public void SetPersonId(Guid personId)
        {
            PersonId = personId;
        }

        public void SetSituation(string situation)
        {
            Situation = situation;
        }

        public void SetDiscountPercentage(decimal discountPercentage)
        {
            DiscountPercentage = discountPercentage;
        }

        public void SetDiscountValue(decimal discountValue)
        {
            DiscountValue = discountValue;
        }

        public void SetTotalBudgetAmount(decimal totalBudgetAmount)
        {
            TotalBudgetAmount = totalBudgetAmount;
        }

        public void SetTotalBudgetedAmount(decimal totalBudgetedAmount)
        {
            TotalBudgetedAmount = totalBudgetedAmount;
        }

        public void SetExpirationDate(DateTime? expirationDate)
        {
            ExpirationDate = expirationDate;
        }

        public void SetCreationDate(DateTime? creationDate)
        {
            CreationDate = creationDate;
        }

        public void SetDetails(string? details)
        {
            Details = details;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
