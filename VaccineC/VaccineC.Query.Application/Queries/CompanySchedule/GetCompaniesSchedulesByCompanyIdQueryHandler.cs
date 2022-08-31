using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.CompanySchedule
{
    public class GetCompaniesSchedulesByCompanyIdQueryHandler : IRequestHandler<GetCompaniesSchedulesByCompanyIdQuery, IEnumerable<CompanyScheduleViewModel>>
    {

        private readonly ICompanyScheduleAppService _companyScheduleAppService;

        public GetCompaniesSchedulesByCompanyIdQueryHandler(ICompanyScheduleAppService companyScheduleAppService)
        {
            _companyScheduleAppService = companyScheduleAppService;
        }
        public async Task<IEnumerable<CompanyScheduleViewModel>> Handle(GetCompaniesSchedulesByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            return await _companyScheduleAppService.GetAllCompaniesSchedulesByCompanyID(request.CompanyID);
        }
    }
}
