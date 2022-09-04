using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonAddress
{
    public class GetPrincipalPersonAddressByPersonIdQueryHandler : IRequestHandler<GetPrincipalPersonAddressByPersonIdQuery, PersonAddressViewModel>
    {
        private readonly IMediator _mediator;

        public GetPrincipalPersonAddressByPersonIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PersonAddressViewModel> Handle(GetPrincipalPersonAddressByPersonIdQuery request, CancellationToken cancellationToken)
        {
            var personsAddresses = await _mediator.Send(new GetPersonAddressListQuery());
            var personAddress = personsAddresses
                .Where(pa => pa.PersonID == request.PersonId && pa.AddressType.Equals("P"))
                .FirstOrDefault();

            return personAddress;
        }
    }
}
