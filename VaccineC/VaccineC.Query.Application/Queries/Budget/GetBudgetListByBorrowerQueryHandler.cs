using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Budget
{
    public class GetBudgetListByBorrowerQueryHandler : IRequestHandler<GetBudgetListByBorrowerQuery, IEnumerable<BudgetViewModel>>
    {

        private readonly IBudgetAppService _appService;

        public GetBudgetListByBorrowerQueryHandler(IBudgetAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetViewModel>> Handle(GetBudgetListByBorrowerQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllByBorrower(request.BorrowerId);
        }
    }
}
