using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetHistoric
{
    public class GetBudgetHistoricListByBudgetQueryHandler : IRequestHandler<GetBudgetHistoricListByBudgetQuery, IEnumerable<BudgetHistoricViewModel>>
    {

        private readonly IBudgetHistoricAppService _appService;

        public GetBudgetHistoricListByBudgetQueryHandler(IBudgetHistoricAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetHistoricViewModel>> Handle(GetBudgetHistoricListByBudgetQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllBudgetsHistoricsByBudgetId(request.BudgetID);

        }
    }
}
