using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetProduct
{
    public class GetBudgetProductListByBudgetQueryHandler : IRequestHandler<GetBudgetProductListByBudgetQuery, IEnumerable<BudgetProductViewModel>>
    {

        private readonly IBudgetProductAppService _appService;

        public GetBudgetProductListByBudgetQueryHandler(IBudgetProductAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(GetBudgetProductListByBudgetQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllBudgetsProductsByBudgetId(request.BudgetID);
        }
    }
}
