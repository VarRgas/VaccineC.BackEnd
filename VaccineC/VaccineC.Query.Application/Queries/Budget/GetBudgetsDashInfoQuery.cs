using MediatR;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.Budget
{
    public class GetBudgetsDashInfoQuery : IRequest<BudgetDashInfoViewModel>
    {
        public int Month;
        public int Year;

        public GetBudgetsDashInfoQuery(int month, int year)
        {
            Month = month;
            Year = year;
        }
    }
}
