using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.CompanySchedule
{
    public class GetCompanyScheduleListQueryHandler : IRequestHandler<GetCompanyScheduleListQuery, IEnumerable<CompanyScheduleViewModel>>
    {
        private readonly ICompanyScheduleAppService _companyScheduleAppService;

        public GetCompanyScheduleListQueryHandler(ICompanyScheduleAppService companyScheduleAppService)
        {
            _companyScheduleAppService = companyScheduleAppService;
        }
        public async Task<IEnumerable<CompanyScheduleViewModel>> Handle(GetCompanyScheduleListQuery request, CancellationToken cancellationToken)
        {
            return await _companyScheduleAppService.GetAllAsync();
        }
    }
}
