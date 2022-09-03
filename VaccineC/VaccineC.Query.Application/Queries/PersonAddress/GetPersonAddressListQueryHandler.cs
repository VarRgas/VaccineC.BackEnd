using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonAddress
{
    public class GetPersonAddressListQueryHandler : IRequestHandler<GetPersonAddressListQuery, IEnumerable<PersonAddressViewModel>>
    {
        private readonly IPersonAddressAppService _personAddressAppService;

        public GetPersonAddressListQueryHandler(IPersonAddressAppService personAddressAppService)
        {
            _personAddressAppService = personAddressAppService;
        }

        public async Task<IEnumerable<PersonAddressViewModel>> Handle(GetPersonAddressListQuery request, CancellationToken cancellationToken)
        {
            return await _personAddressAppService.GetAllAsync();
        }
    }
}
