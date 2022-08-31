using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.CompanySchedule
{
    public class GetCompanyScheduleListQuery : IRequest<IEnumerable<CompanyScheduleViewModel>>
    {
    }
}
