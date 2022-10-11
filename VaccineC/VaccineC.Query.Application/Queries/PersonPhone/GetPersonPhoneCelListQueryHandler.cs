using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonPhone
{
    public class GetPersonPhoneCelListQueryHandler : IRequestHandler<GetPersonPhoneCelListQuery, IEnumerable<PersonPhoneViewModel>>
    {
        private readonly IPersonPhoneAppService _personPhoneAppService;

        public GetPersonPhoneCelListQueryHandler(IPersonPhoneAppService personPhoneAppService)
        {
            _personPhoneAppService = personPhoneAppService;
        }

        public async Task<IEnumerable<PersonPhoneViewModel>> Handle(GetPersonPhoneCelListQuery request, CancellationToken cancellationToken)
        {
            return await _personPhoneAppService.GetAllPersonsPhonesCelByPersonId(request.PersonID);
        }
    }
}
