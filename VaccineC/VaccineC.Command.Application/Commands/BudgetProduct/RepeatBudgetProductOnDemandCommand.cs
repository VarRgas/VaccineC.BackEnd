using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetProduct
{
    public class RepeatBudgetProductOnDemandCommand : IRequest<IEnumerable<BudgetProductViewModel>>
    {

        public int NumberOfTimes;
        public Guid BudgetProductId;
        public Boolean RepeatBorrower;
        public Guid? UserId;

        public RepeatBudgetProductOnDemandCommand(int numberOfTimes, Guid budgetProductId, Boolean repeatBorrower, Guid? userId)
        {
            NumberOfTimes = numberOfTimes;
            BudgetProductId = budgetProductId;
            RepeatBorrower = repeatBorrower;
            UserId = userId;    
        }
    }
}
