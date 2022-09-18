using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetProduct
{
    public class GetBudgetProductListQuery : IRequest<IEnumerable<BudgetProductViewModel>>
    {
    }
}
