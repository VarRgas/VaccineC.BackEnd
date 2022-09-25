using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class BudgetNegotiation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("budgetId")]
        public Guid BudgetId { get; set; }

        [Column("paymentFormId")]
        public Guid PaymentFormId { get; set; }

        [Column("totalAmountBalance", TypeName = "numeric(15,2)")]
        public decimal TotalAmountBalance { get; set; }

        [Column("totalAmountTraded", TypeName = "numeric(15,2)")]
        public decimal TotalAmountTraded { get; set; }

        [Column("installments", TypeName = "int")]
        public int Installments { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public BudgetNegotiation(Guid id, Guid budgetId, Guid paymentFormId, decimal totalAmountBalance, decimal totalAmountTraded, int installments, DateTime register)
        {
            ID = id;
            BudgetId = budgetId;
            PaymentFormId = paymentFormId;
            TotalAmountBalance = totalAmountBalance;
            TotalAmountTraded = totalAmountTraded;
            Installments = installments;
            Register = register;
        }

        public BudgetNegotiation()
        {

        }

        public void SetBudgetId(Guid budgetId)
        {
            BudgetId = budgetId;
        }

        public void SetPaymentFormId(Guid paymentFormId)
        {
            PaymentFormId = paymentFormId;
        }
        public void SetTotalAmountBalance(decimal totalAmountBalance)
        {
            TotalAmountBalance = totalAmountBalance;
        }

        public void SetTotalAmountTraded(decimal totalAmountTraded)
        {
            TotalAmountTraded = totalAmountTraded;
        }

        public void SetInstallments(int installments)
        {
            Installments = installments;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
