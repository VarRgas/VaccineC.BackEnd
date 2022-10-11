using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetProduct
{
    public class GetPendingBudgetProductListByResponsibleQuery : IRequest<IEnumerable<BudgetProductViewModel>>
    {
        public Guid BudgetId;

        public GetPendingBudgetProductListByResponsibleQuery(Guid budgetId)
        {
            BudgetId = budgetId;
        }
    }
}
