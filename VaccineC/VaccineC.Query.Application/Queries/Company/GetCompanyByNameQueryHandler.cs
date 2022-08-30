using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Company
{
    public class GetCompanyByNameQueryHandler : IRequestHandler<GetCompanyByNameQuery, IEnumerable<CompanyViewModel>>
    {
        private readonly ICompanyAppService _companyAppService;

        public GetCompanyByNameQueryHandler(ICompanyAppService companyAppService)
        {
            _companyAppService = companyAppService;
        }

        public async Task<IEnumerable<CompanyViewModel>> Handle(GetCompanyByNameQuery request, CancellationToken cancellationToken)
        {
            return await _companyAppService.GetByName(request.Name);
        }

    }
}
