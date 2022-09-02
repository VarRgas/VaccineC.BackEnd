using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Person
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonViewModel>
    {

        private readonly IMediator _mediator;

        public GetPersonByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PersonViewModel> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var companies = await _mediator.Send(new GetPersonListQuery());
            var company = companies.FirstOrDefault(pf => pf.ID == request.Id);
            return company;

        }
    }
}
