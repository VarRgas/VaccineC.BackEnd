using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Company
{
    public class GetCompaniesParametersByCompanyIDQueryHandler : IRequestHandler<GetCompaniesParametersByCompanyIDQuery, IEnumerable<CompaniesParametersViewModel>>
    {

        private readonly ICompanyAppService _companyAppService;

        public GetCompaniesParametersByCompanyIDQueryHandler(ICompanyAppService companyAppService)
        {
            _companyAppService = companyAppService;
        }

        public async Task<IEnumerable<CompaniesParametersViewModel>> Handle(GetCompaniesParametersByCompanyIDQuery request, CancellationToken cancellationToken)
        {
            return await _companyAppService.GetAllParametersByCompanyID(request.ID);
        }
    }
}
