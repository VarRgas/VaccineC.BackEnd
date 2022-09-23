using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Budget
{
    public class GetBudgetByPersonNameQuery : IRequest<IEnumerable<BudgetViewModel>>
    {
        public string PersonName { get; set; }

        public GetBudgetByPersonNameQuery(string personName)
        {
            PersonName = personName;
        }
    }
}
