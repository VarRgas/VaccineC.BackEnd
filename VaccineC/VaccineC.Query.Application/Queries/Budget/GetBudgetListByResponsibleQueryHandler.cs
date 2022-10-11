using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Budget
{
    public class GetBudgetListByResponsibleQueryHandler : IRequestHandler<GetBudgetListByResponsibleQuery, IEnumerable<BudgetViewModel>>
    {

        private readonly IBudgetAppService _appService;

        public GetBudgetListByResponsibleQueryHandler(IBudgetAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetViewModel>> Handle(GetBudgetListByResponsibleQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllByResponsible(request.ResponsibleId);
        }
    }
}
