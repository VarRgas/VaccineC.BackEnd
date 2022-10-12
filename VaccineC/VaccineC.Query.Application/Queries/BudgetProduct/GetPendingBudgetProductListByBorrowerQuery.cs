using MediatR;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.BudgetProduct
{
    public class GetPendingBudgetProductListByBorrowerQuery : IRequest<IEnumerable<BudgetProductViewModel>>
    {
        public Guid BudgetId;
        public Guid BorrowerId;
        public DateTime StartDate;

        public GetPendingBudgetProductListByBorrowerQuery(Guid budgetId, Guid borrowerId, DateTime startDate)
        {
            BudgetId = budgetId;
            BorrowerId = borrowerId;
            StartDate = startDate;
        }
    }
}
