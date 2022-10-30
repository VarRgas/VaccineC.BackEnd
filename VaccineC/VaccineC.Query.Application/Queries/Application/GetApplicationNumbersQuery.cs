using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationNumbersQuery : IRequest<ApplicationNumberViewModel>
    {
        public int Month;
        public int Year;

        public GetApplicationNumbersQuery(int month, int year)
        {
            Month = month;
            Year = year;
        }
    }
}
