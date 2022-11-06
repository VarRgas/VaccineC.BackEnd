using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Budget
{
    public class UpdateBudgetCommand : IRequest<BudgetViewModel>
    {
        public Guid ID;
        public Guid UserID;
        public Guid PersonID;
        public string Situation;
        public decimal DiscountPercentage;
        public decimal DiscountValue;
        public decimal TotalBudgetAmount;
        public decimal TotalBudgetedAmount;
        public DateTime? ExpirationDate;
        public DateTime? CreationDate;
        public string? Details;
        public int BudgetNumber;
        public DateTime Register;

        public UpdateBudgetCommand(Guid id, Guid userID, Guid personID, string situation, decimal discountPercentage, decimal discountValue, decimal totalBudgetAmount, decimal totalBudgetedAmount, DateTime? expirationDate, DateTime? creationDate, string? details, int budgetNumber, DateTime register)
        {
            ID = id;
            UserID = userID;
            PersonID = personID;
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
    }
}
