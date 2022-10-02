using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetHistoric
{
    public class GetBudgetHistoricListQuery : IRequest<IEnumerable<BudgetHistoricViewModel>>
    {
    }
}
