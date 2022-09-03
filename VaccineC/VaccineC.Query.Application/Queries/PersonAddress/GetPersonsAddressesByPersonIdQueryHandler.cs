using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonAddress
{
    public class GetPersonsAddressesByPersonIdQueryHandler : IRequestHandler<GetPersonsAddressesByPersonIdQuery, IEnumerable<PersonAddressViewModel>>
    {
        private readonly IPersonAddressAppService _personAddressAppService;

        public GetPersonsAddressesByPersonIdQueryHandler(IPersonAddressAppService personAddressAppService)
        {
            _personAddressAppService = personAddressAppService;
        }

        public async Task<IEnumerable<PersonAddressViewModel>> Handle(GetPersonsAddressesByPersonIdQuery request, CancellationToken cancellationToken)
        {
            return await _personAddressAppService.GetAllPersonsAddressesByPersonId(request.PersonID);
        }
    }
}
