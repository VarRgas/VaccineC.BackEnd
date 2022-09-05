using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonJuridical
{
    public class GetPersonJuridicalByPersonIdQueryHandler : IRequestHandler<GetPersonJuridicalByPersonIdQuery, PersonsJuridicalViewModel>
    {
        private readonly IMediator _mediator;

        public GetPersonJuridicalByPersonIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PersonsJuridicalViewModel> Handle(GetPersonJuridicalByPersonIdQuery request, CancellationToken cancellationToken)
        {
            var personsPhysicals = await _mediator.Send(new GetPersonJuridicalListQuery());
            var personPhysical = personsPhysicals
                .Where(pp => pp.PersonID == request.PersonId)
                .FirstOrDefault();

            return personPhysical;
        }
    }
}
