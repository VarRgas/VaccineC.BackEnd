using AutoMapper;
using MediatR;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetAuthorizationByEventIdQueryHandler : IRequestHandler<GetAuthorizationByEventIdQuery, AuthorizationViewModel>
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public GetAuthorizationByEventIdQueryHandler(IMediator mediator, IMapper mapper, VaccineCContext context)
        {
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<AuthorizationViewModel> Handle(GetAuthorizationByEventIdQuery request, CancellationToken cancellationToken)
        {
            var authorizations = await _mediator.Send(new GetAuthorizationListQuery());
            var authorization = authorizations.FirstOrDefault(a => a.EventId == request.EventId);

            var person = authorization.Person;

            var personPhone = (from pp in _context.PersonsPhones
                               where pp.PhoneType.Equals("P") && pp.PersonID.Equals(person.ID)
                               select pp).FirstOrDefault();

            var personPhoneViewModel = _mapper.Map<PersonPhoneViewModel>(personPhone);
            person.PersonPrincipalPhone = personPhoneViewModel;


            var personAddress = (from pa in _context.PersonsAddresses
                                 where pa.AddressType.Equals("P") && pa.PersonID.Equals(person.ID)
                                 select pa).FirstOrDefault();

            var personAddressViewModel = _mapper.Map<PersonAddressViewModel>(personAddress);
            person.PersonPrincipalAddress = personAddressViewModel;

            authorization.Person.PersonPrincipalPhone = personPhoneViewModel;
            authorization.Person.PersonPrincipalAddress = personAddressViewModel;

            return authorization;
        }
    }
}
