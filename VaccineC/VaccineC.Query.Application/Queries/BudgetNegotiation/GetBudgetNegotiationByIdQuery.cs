using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetNegotiation
{
    public class GetBudgetNegotiationByIdQuery : IRequest<BudgetNegotiationViewModel>
    {
        public Guid Id;

        public GetBudgetNegotiationByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
