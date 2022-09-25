using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetNegotiation
{
    public class AddBudgetNegotiationCommand : IRequest<IEnumerable<BudgetNegotiationViewModel>>
    {
        public Guid ID;
        public Guid BudgetId;
        public Guid PaymentFormId;
        public decimal TotalAmountBalance;
        public decimal TotalAmountTraded;
        public int Installments;
        public DateTime Register;

        public AddBudgetNegotiationCommand(Guid id, Guid budgetId, Guid paymentFormId, decimal totalAmountBalance, decimal totalAmountTraded, int installments, DateTime register)
        {
            ID = id;
            BudgetId = budgetId;
            PaymentFormId = paymentFormId;
            TotalAmountBalance = totalAmountBalance;
            TotalAmountTraded = totalAmountTraded;
            Installments = installments;
            Register = register;
        }
    }
}
