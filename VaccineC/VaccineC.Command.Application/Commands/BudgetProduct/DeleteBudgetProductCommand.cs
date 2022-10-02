using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetProduct
{
    public class DeleteBudgetProductCommand : IRequest<IEnumerable<BudgetProductViewModel>>
    {
        public Guid Id;
        public Guid UserId;
        public DeleteBudgetProductCommand(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
