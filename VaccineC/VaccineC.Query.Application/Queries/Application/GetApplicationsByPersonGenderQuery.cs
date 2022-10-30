using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationsByPersonGenderQuery : IRequest<IEnumerable<ApplicationPersonGenderViewModel>>
    {
        public int Month;
        public int Year;

        public GetApplicationsByPersonGenderQuery(int month, int year)
        {
            Month = month;
            Year = year;
        }
    }
}
