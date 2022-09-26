using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetProduct
{
    public class AddBudgetProductOnDemandCommand : IRequest<IEnumerable<BudgetProductViewModel>>
    {

        public List<BudgetProductViewModel> ListBudgetProdcutViewModel;

        public AddBudgetProductOnDemandCommand(List<BudgetProductViewModel> listBudgetProdcutViewModel)
        {
            ListBudgetProdcutViewModel = listBudgetProdcutViewModel;
        }
    }
}
