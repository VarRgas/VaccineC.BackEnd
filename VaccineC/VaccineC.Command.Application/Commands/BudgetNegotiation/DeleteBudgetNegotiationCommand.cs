using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetNegotiation
{
    public class DeleteBudgetNegotiationCommand : IRequest<IEnumerable<BudgetNegotiationViewModel>>
    {
        public Guid Id;
        public Guid? UserId;
        public DeleteBudgetNegotiationCommand(Guid id, Guid? userId)
        {
            Id = id;
            UserId = userId;    
        }
    }
}
