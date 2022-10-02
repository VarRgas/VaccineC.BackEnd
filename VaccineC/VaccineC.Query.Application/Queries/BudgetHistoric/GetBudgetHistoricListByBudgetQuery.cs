using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetHistoric
{
    public class GetBudgetHistoricListByBudgetQuery : IRequest<IEnumerable<BudgetHistoricViewModel>>
    {
        public Guid BudgetID { get; set; }

        public GetBudgetHistoricListByBudgetQuery(Guid budgetId)
        {
            BudgetID = budgetId;
        }
    }
}
