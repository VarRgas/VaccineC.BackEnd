using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonAddress
{
    public class GetPersonAddressByIdQueryHandler : IRequestHandler<GetPersonAddressByIdQuery, PersonAddressViewModel>
    {
        private readonly IMediator _mediator;

        public GetPersonAddressByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PersonAddressViewModel> Handle(GetPersonAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var personAddresses = await _mediator.Send(new GetPersonAddressListQuery());
            var personAddress = personAddresses.FirstOrDefault(pf => pf.ID == request.Id);
            return personAddress;
        }
    }
}
