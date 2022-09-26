using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class BudgetProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("budgetId")]
        public Guid BudgetId { get; set; }

        [Column("productId")]
        public Guid ProductId { get; set; }

        [Column("borrowerPersonId")]
        public Guid? BorrowerPersonId { get; set; }

        [Column("productDose", TypeName = "varchar(2)")]
        public string? ProductDose { get; set; }

        [Column("details", TypeName = "varchar(1000)")]
        public string? Details { get; set; }

        [Column("estimatedSalesValue", TypeName = "numeric(15,2)")]
        public decimal EstimatedSalesValue { get; set; }

        [Column("situationProduct", TypeName = "varchar(1)")]
        public string SituationProduct { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public BudgetProduct(Guid id, Guid budgetId, Guid productId, Guid? borrowerPersonId, string? productDose, string? details, decimal estimatedSalesValue, string situationProduct, DateTime register)
        {
            ID = id;
            BudgetId = budgetId;
            ProductId = productId;
            BorrowerPersonId = borrowerPersonId;
            ProductDose = productDose;
            Details = details;
            EstimatedSalesValue = estimatedSalesValue;
            SituationProduct = situationProduct;
            Register = register;
        }

        public BudgetProduct()
        {

        }
        public void SetBudgetId(Guid budgetId)
        {
            BudgetId = budgetId;
        }
        public void SetProductId(Guid productId)
        {
            ProductId = productId;
        }
        public void SetBorrowerPersonId(Guid? borrowerPersonId)
        {
            BorrowerPersonId = borrowerPersonId;
        }
        public void SetProductDose(string? productDose)
        {
            ProductDose = productDose;
        }
        public void SetDetails(string details)
        {
            Details = details;
        }
        public void SetEstimatedSalesValue(decimal estimatedSalesValue)
        {
            EstimatedSalesValue = estimatedSalesValue;
        }
        public void SetSituationProduct(string situationProduct)
        {
            SituationProduct = situationProduct;
        }
        public void SetRegister(DateTime register)
        {
            Register = register;
        }

    }
}
