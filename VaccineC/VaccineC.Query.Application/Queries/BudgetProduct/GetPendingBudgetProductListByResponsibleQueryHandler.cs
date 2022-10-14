using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetProduct
{
    public class GetPendingBudgetProductListByResponsibleQueryHandler : IRequestHandler<GetPendingBudgetProductListByResponsibleQuery, IEnumerable<BudgetProductViewModel>>
    {

        private readonly IBudgetProductAppService _appService;

        public GetPendingBudgetProductListByResponsibleQueryHandler(IBudgetProductAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(GetPendingBudgetProductListByResponsibleQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllPendingBudgetsProductsByResponsible(request.BudgetId, request.StartDate);
        }
    }
}
