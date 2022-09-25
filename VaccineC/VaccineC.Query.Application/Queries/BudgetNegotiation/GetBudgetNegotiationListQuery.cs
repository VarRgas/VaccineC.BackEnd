using MediatR;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.BudgetNegotiation
{
    public class GetBudgetNegotiationListQuery : IRequest<IEnumerable<BudgetNegotiationViewModel>>
    {
    }
}
