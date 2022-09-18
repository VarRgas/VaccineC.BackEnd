using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetProduct
{
    public class GetBudgetProductListQueryHandler : IRequestHandler<GetBudgetProductListQuery, IEnumerable<BudgetProductViewModel>>
    {

        private readonly IBudgetProductAppService _appService;

        public GetBudgetProductListQueryHandler(IBudgetProductAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(GetBudgetProductListQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllAsync();
        }
    }
}
