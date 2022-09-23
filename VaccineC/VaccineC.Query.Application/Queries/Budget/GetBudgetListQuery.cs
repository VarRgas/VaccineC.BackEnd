using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Budget
{
    public class GetBudgetListQuery : IRequest<IEnumerable<BudgetViewModel>>
    {
    }
}
