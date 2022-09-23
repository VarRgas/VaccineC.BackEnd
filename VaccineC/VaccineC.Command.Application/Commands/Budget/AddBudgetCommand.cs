using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Budget
{
    public class AddBudgetCommand : IRequest<BudgetViewModel>
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
        public DateTime? ApprovalDate;
        public string? Details;
        public int BudgetNumber;
        public DateTime Register;
        public AddBudgetCommand(Guid id, Guid userId, Guid personId, string situation, decimal discountPercentage, decimal discountValue,
            decimal totalBudgetAmount, decimal totalBudgetedAmount, DateTime? expirationDate, DateTime? approvalDate, string? details, int budgetNumber, DateTime register)
        {
            ID = id;
            UserID = userId;
            PersonID = personId;
            Situation = situation;
            DiscountPercentage = discountPercentage;
            DiscountValue = discountValue;
            TotalBudgetAmount = totalBudgetAmount;
            TotalBudgetedAmount = totalBudgetedAmount;
            ExpirationDate = expirationDate;
            ApprovalDate = approvalDate;
            Details = details;
            BudgetNumber = budgetNumber;
            Register = register;
        }
    }
}
