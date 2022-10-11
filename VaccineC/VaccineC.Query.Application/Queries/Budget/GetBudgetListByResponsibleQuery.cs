using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Budget
{
    public class GetBudgetListByResponsibleQuery : IRequest<IEnumerable<BudgetViewModel>>
    {
        public Guid ResponsibleId;

        public GetBudgetListByResponsibleQuery(Guid responsibleId)
        {
            ResponsibleId = responsibleId;
        }
    }
}
