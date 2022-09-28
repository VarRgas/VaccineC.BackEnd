using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetNegotiation
{
    public class DeleteBudgetNegotiationOnDemandCommand : IRequest<IEnumerable<BudgetNegotiationViewModel>>
    {

        public List<BudgetNegotiationViewModel> ListBudgetNegotiationViewModel;

        public DeleteBudgetNegotiationOnDemandCommand(List<BudgetNegotiationViewModel> listBudgetNegotiationViewModel)
        {
            ListBudgetNegotiationViewModel = listBudgetNegotiationViewModel;
        }
    }
}
