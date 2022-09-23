using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Budget
{
    public class GetBudgetListQueryHandler : IRequestHandler<GetBudgetListQuery, IEnumerable<BudgetViewModel>>
    {

        private readonly IBudgetAppService _appService;

        public GetBudgetListQueryHandler(IBudgetAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetViewModel>> Handle(GetBudgetListQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllAsync();
        }
    }
}
