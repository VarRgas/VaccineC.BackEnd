using MediatR;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationsByProductIdQuery : IRequest<IEnumerable<ApplicationProductViewModel>>
    {
        public int Month;
        public int Year;

        public GetApplicationsByProductIdQuery(int month, int year)
        {
            Month = month;
            Year = year;
        }
    }
}
