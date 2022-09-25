using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.CompanyParameter
{
    public class GetCompanyParameterByIdQueryHandler : IRequestHandler<GetCompanyParameterByIdQuery, CompaniesParametersViewModel>
    {
        private readonly IMediator _mediator;

        public GetCompanyParameterByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CompaniesParametersViewModel> Handle(GetCompanyParameterByIdQuery request, CancellationToken cancellationToken)
        {
            var companiesParameters = await _mediator.Send(new GetCompanyParameterListQuery());
            var companyParameter = companiesParameters.FirstOrDefault(pf => pf.ID == request.Id);
            return companyParameter;
        }
    }
}
