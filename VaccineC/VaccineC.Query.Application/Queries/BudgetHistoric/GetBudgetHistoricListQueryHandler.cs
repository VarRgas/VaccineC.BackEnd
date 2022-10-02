using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.BudgetHistoric
{
    public class GetBudgetHistoricListQueryHandler : IRequestHandler<GetBudgetHistoricListQuery, IEnumerable<BudgetHistoricViewModel>>
    {

        private readonly IBudgetHistoricAppService _appService;

        public GetBudgetHistoricListQueryHandler(IBudgetHistoricAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetHistoricViewModel>> Handle(GetBudgetHistoricListQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllAsync();
        }
    }
}
