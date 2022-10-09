using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetProduct
{
    public class GetPendingBudgetProductListByBorrowerQueryHandler : IRequestHandler<GetPendingBudgetProductListByBorrowerQuery, IEnumerable<BudgetProductViewModel>>
    {

        private readonly IBudgetProductAppService _appService;

        public GetPendingBudgetProductListByBorrowerQueryHandler(IBudgetProductAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(GetPendingBudgetProductListByBorrowerQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllPendingBudgetsProductsByBorrower(request.BudgetId, request.BorrowerId);
        }
    }
}
