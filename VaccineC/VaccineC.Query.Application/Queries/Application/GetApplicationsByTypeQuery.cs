using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationsByTypeQuery : IRequest<IEnumerable<ApplicationTypeViewModel>>
    {
        public int Month;
        public int Year;

        public GetApplicationsByTypeQuery(int month, int year)
        {
            Month = month;
            Year = year;
        }
    }
}
