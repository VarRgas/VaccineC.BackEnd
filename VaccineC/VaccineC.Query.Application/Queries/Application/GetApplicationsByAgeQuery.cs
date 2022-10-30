using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationsByAgeQuery : IRequest<IEnumerable<ApplicationPersonAgeViewModel>>
    {
        public int Month;
        public int Year;

        public GetApplicationsByAgeQuery(int month, int year)
        {
            Month = month;
            Year = year;
        }
    }
}
