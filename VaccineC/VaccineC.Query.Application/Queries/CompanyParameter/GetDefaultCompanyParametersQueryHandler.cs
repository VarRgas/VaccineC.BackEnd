using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.CompanyParameter
{
    public class GetDefaultCompanyParametersQueryHandler : IRequestHandler<GetDefaultCompanyParametersQuery, CompaniesParametersViewModel>
    {
        private readonly IMediator _mediator;

        public GetDefaultCompanyParametersQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CompaniesParametersViewModel> Handle(GetDefaultCompanyParametersQuery request, CancellationToken cancellationToken)
        {
            var companiesParameters = await _mediator.Send(new GetCompanyParameterListQuery());
            var companyParameter = companiesParameters.FirstOrDefault();
            return companyParameter;
        }
    }
}
