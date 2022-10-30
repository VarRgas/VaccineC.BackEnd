using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetSipniIntegrationSituationQuery : IRequest<IEnumerable<ApplicationSipniIntegrationViewModel>>
    {
        public int Month;
        public int Year;

        public GetSipniIntegrationSituationQuery(int month, int year)
        {
            Month = month;
            Year = year;
        }
    }
}
