using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetProduct
{
    public class DeleteBudgetProductCommand : IRequest<IEnumerable<BudgetProductViewModel>>
    {
        public Guid Id;
        public DeleteBudgetProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
