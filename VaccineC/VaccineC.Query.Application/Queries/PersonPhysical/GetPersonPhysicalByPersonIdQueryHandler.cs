using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonPhysical
{
    public class GetPersonPhysicalByPersonIdQueryHandler : IRequestHandler<GetPersonPhysicalByPersonIdQuery, PersonsPhysicalViewModel>
    {
        private readonly IMediator _mediator;

        public GetPersonPhysicalByPersonIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PersonsPhysicalViewModel> Handle(GetPersonPhysicalByPersonIdQuery request, CancellationToken cancellationToken)
        {
            var personsPhysicals = await _mediator.Send(new GetPersonPhysicalListQuery());
            var personPhysical = personsPhysicals
                .Where(pp => pp.PersonID == request.PersonId)
                .FirstOrDefault();

            return personPhysical;
        }
    }
}
