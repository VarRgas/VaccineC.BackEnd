using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetHistoric
{
    public class AddBudgetHistoricCommand : IRequest
    {
        public Guid ID;
        public Guid BudgetId;
        public Guid? UserId;
        public string Historic;
        public DateTime Register;

        public AddBudgetHistoricCommand(Guid id, Guid budgetId, Guid? userId, string historic, DateTime register)
        {
            ID = id;
            BudgetId = budgetId;
            UserId = userId;
            Historic = historic;
            Register = register;
        }

    }
}
