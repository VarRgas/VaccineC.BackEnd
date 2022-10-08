using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Budget
{
    public class GetBudgetListByBorrowerQuery : IRequest<IEnumerable<BudgetViewModel>>
    {
        public Guid BorrowerId;

        public GetBudgetListByBorrowerQuery(Guid borrowerId)
        {
            BorrowerId = borrowerId;
        }
    }
}
