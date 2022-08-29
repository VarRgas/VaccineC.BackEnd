using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Company
{
    public class GetCompanyListQueryHandler : IRequestHandler<GetCompanyListQuery, IEnumerable<CompanyViewModel>>
    {

        private readonly ICompanyAppService _companyAppService;

        public GetCompanyListQueryHandler(ICompanyAppService companyAppService)
        {
            _companyAppService = companyAppService;
        }

        public async Task<IEnumerable<CompanyViewModel>> Handle(GetCompanyListQuery request, CancellationToken cancellationToken)
        {
            return await _companyAppService.GetAllAsync();
        }
    }
}
