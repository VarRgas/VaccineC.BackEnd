using MediatR;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.PersonPhone
{
    public class GetPrincipalPersonPhoneByPersonIdQueryHandler : IRequestHandler<GetPrincipalPersonPhoneByPersonIdQuery, PersonPhoneViewModel>
    {
        private readonly IMediator _mediator;

        public GetPrincipalPersonPhoneByPersonIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PersonPhoneViewModel> Handle(GetPrincipalPersonPhoneByPersonIdQuery request, CancellationToken cancellationToken)
        {
            var personsPhones = await _mediator.Send(new GetPersonPhoneListQuery());
            var personPhone = personsPhones
                .Where(pp => pp.PersonID == request.PersonId && pp.PhoneType.Equals("P"))
                .FirstOrDefault();

            return personPhone;
        }
    }
}
