using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetNegotiation
{
    public class GetBudgetNegotiationListByBudgetQuery : IRequest<IEnumerable<BudgetNegotiationViewModel>>
    {
        public Guid BudgetID { get; set; }

        public GetBudgetNegotiationListByBudgetQuery(Guid budgetId)
        {
            BudgetID = budgetId;
        }
    }
}
