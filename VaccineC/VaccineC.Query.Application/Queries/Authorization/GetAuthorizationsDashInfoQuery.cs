using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetAuthorizationsDashInfoQuery : IRequest<AuthorizationDashInfoViewModel>
    {
        public int Month;
        public int Year;

        public GetAuthorizationsDashInfoQuery(int month, int year)
        {
            Month = month;
            Year = year;
        }
    }
}
