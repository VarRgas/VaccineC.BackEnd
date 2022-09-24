using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetProduct
{
    public class GetBudgetProductListByBudgetQuery : IRequest<IEnumerable<BudgetProductViewModel>>
    {
        public Guid BudgetID { get; set; }

        public GetBudgetProductListByBudgetQuery(Guid budgetId)
        {
            BudgetID = budgetId;
        }
    }
}
