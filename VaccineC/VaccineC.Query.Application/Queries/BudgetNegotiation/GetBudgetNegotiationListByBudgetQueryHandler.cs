using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetNegotiation
{
    public class GetBudgetNegotiationListByBudgetQueryHandler : IRequestHandler<GetBudgetNegotiationListByBudgetQuery, IEnumerable<BudgetNegotiationViewModel>>
    {

        private readonly IBudgetNegotiationAppService _appService;

        public GetBudgetNegotiationListByBudgetQueryHandler(IBudgetNegotiationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetNegotiationViewModel>> Handle(GetBudgetNegotiationListByBudgetQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllBudgetsNegotiationsByBudgetId(request.BudgetID);
        }
    }
}
