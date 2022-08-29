using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Company
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyViewModel>
    {

        private readonly IMediator _mediator;

        public GetCompanyByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CompanyViewModel> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var companies = await _mediator.Send(new GetCompanyListQuery());
            var company = companies.FirstOrDefault(pf => pf.ID == request.Id);
            return company;

        }
    }
}
