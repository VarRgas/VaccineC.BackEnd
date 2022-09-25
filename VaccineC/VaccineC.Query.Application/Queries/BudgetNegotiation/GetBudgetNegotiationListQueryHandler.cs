using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetNegotiation
{
    public class GetBudgetNegotiationListQueryHandler : IRequestHandler<GetBudgetNegotiationListQuery, IEnumerable<BudgetNegotiationViewModel>>
    {

        private readonly IBudgetNegotiationAppService _appService;

        public GetBudgetNegotiationListQueryHandler(IBudgetNegotiationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetNegotiationViewModel>> Handle(GetBudgetNegotiationListQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllAsync();
        }
    }
}
