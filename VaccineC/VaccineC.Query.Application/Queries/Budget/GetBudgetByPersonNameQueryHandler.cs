using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Budget
{
    public class GetBudgetByPersonNameQueryHandler : IRequestHandler<GetBudgetByPersonNameQuery, IEnumerable<BudgetViewModel>>
    {
        private readonly IBudgetAppService _appService;

        public GetBudgetByPersonNameQueryHandler(IBudgetAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetViewModel>> Handle(GetBudgetByPersonNameQuery request, CancellationToken cancellationToken)
        {
            long n;
            bool isNumeric = long.TryParse(request.PersonName, out n);

            if (isNumeric)
            {
                int budgetNumber = int.Parse(request.PersonName);
                return await _appService.GetAllByBudgetNumber(budgetNumber);

            }
            else
            {
                return await _appService.GetByName(request.PersonName);

            }

        }

    }
}
