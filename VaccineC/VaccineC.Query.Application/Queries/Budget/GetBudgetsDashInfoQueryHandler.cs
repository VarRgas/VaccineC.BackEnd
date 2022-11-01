using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Budget
{
    public class GetBudgetsDashInfoQueryHandler : IRequestHandler<GetBudgetsDashInfoQuery, BudgetDashInfoViewModel>
    {

        private readonly IBudgetAppService _appService;

        public GetBudgetsDashInfoQueryHandler(IBudgetAppService appService)
        {
            _appService = appService;
        }

        public async Task<BudgetDashInfoViewModel> Handle(GetBudgetsDashInfoQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetBudgetsDashInfo(request.Month, request.Year);
        }
    }
}
