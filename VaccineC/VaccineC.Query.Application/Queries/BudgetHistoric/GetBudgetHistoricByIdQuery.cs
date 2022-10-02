using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetHistoric
{
    public class GetBudgetHistoricByIdQuery : IRequest<BudgetHistoricViewModel>
    {
        public Guid Id;

        public GetBudgetHistoricByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
