using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetProduct
{
    public class GetPendingBudgetProductListByResponsibleQuery : IRequest<IEnumerable<BudgetProductViewModel>>
    {
        public Guid BudgetId;
        public DateTime StartDate;

        public GetPendingBudgetProductListByResponsibleQuery(Guid budgetId, DateTime startDate)
        {
            BudgetId = budgetId;
            StartDate = startDate;
        }
    }
}
