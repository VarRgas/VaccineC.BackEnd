using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.CompanySchedule
{
    public class GetCompanyScheduleByIdQuery : IRequest<CompanyScheduleViewModel>
    {
        public Guid Id;

        public GetCompanyScheduleByIdQuery(Guid id)
        {
            Id = id;
        }

    }
}
