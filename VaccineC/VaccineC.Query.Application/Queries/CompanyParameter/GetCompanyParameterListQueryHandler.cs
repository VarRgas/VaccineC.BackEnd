using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.CompanyParameter
{
    public class GetCompanyParameterListQueryHandler : IRequestHandler<GetCompanyParameterListQuery, IEnumerable<CompaniesParametersViewModel>>
    {

        private readonly ICompanyParameterAppService _companyParameterAppService;

        public GetCompanyParameterListQueryHandler(ICompanyParameterAppService companyParameterAppService)
        {
            _companyParameterAppService = companyParameterAppService;
        }

        public async Task<IEnumerable<CompaniesParametersViewModel>> Handle(GetCompanyParameterListQuery request, CancellationToken cancellationToken)
        {
            return await _companyParameterAppService.GetAllAsync();
        }
    }
}
