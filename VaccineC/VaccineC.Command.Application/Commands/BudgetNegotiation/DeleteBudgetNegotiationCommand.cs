using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetNegotiation
{
    public class DeleteBudgetNegotiationCommand : IRequest<IEnumerable<BudgetNegotiationViewModel>>
    {
        public Guid Id;
        public DeleteBudgetNegotiationCommand(Guid id)
        {
            Id = id;
        }
    }
}
