using MediatR;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.BudgetProduct
{
    public class GetPendingBudgetProductListByBorrowerQuery : IRequest<IEnumerable<BudgetProductViewModel>>
    {
        public Guid BudgetId;
        public Guid BorrowerId;

        public GetPendingBudgetProductListByBorrowerQuery(Guid budgetId, Guid borrowerId)
        {
            BudgetId = budgetId;
            BorrowerId = borrowerId;    
        }
    }
}
