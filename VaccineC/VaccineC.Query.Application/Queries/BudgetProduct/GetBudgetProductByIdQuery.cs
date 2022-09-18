using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetProduct
{
    public class GetBudgetProductByIdQuery : IRequest<BudgetProductViewModel>
    {
        public Guid Id;

        public GetBudgetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
