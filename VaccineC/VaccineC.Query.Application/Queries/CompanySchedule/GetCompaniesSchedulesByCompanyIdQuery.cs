using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.CompanySchedule
{
    public class GetCompaniesSchedulesByCompanyIdQuery : IRequest<IEnumerable<CompanyScheduleViewModel>>
    {
        public Guid CompanyID { get; set; }

        public GetCompaniesSchedulesByCompanyIdQuery(Guid companyId)
        {
            CompanyID = companyId;
        }
    }
}
