using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetNegotiation
{
    public class DeleteBudgetNegotiationOnDemandCommand : IRequest<IEnumerable<BudgetNegotiationViewModel>>
    {

        public List<BudgetNegotiationViewModel> ListBudgetNegotiationViewModel;
        public Guid? userId;
        public DeleteBudgetNegotiationOnDemandCommand(List<BudgetNegotiationViewModel> listBudgetNegotiationViewModel, Guid? userId)
        {
            ListBudgetNegotiationViewModel = listBudgetNegotiationViewModel;
            this.userId = userId;   
        }
    }
}
