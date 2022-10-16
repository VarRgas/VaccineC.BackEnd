using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetProduct
{
    public class UpdateBudgetProductBorrowerPersonCommand : IRequest
    {
        public Guid ID;
        public Guid? BorrowerPersonId;
        public Guid? UserId;

        public UpdateBudgetProductBorrowerPersonCommand(Guid id, Guid? borrowerPersonId, Guid? userId)
        {
            ID = id;
            BorrowerPersonId = borrowerPersonId;
            UserId = userId;
        }
    }
}
