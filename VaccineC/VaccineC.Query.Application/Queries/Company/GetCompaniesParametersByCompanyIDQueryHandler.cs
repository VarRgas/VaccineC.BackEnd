using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Company
{
    public class GetCompaniesParametersByCompanyIDQueryHandler : IRequestHandler<GetCompaniesParametersByCompanyIDQuery, CompaniesParametersViewModel>
    {

        private readonly ICompanyAppService _companyAppService;

        public GetCompaniesParametersByCompanyIDQueryHandler(ICompanyAppService companyAppService, IMediator mediator)
        {
            _companyAppService = companyAppService;
 
        }

        public Task<CompaniesParametersViewModel> Handle(GetCompaniesParametersByCompanyIDQuery request, CancellationToken cancellationToken)
        {
            return _companyAppService.GetAllParametersByCompanyID(request.ID);
        }
    }
}
