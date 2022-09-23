using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Budget
{
    public class GetBudgetByIdQuery : IRequest<BudgetViewModel>
    {
        public Guid Id;

        public GetBudgetByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
