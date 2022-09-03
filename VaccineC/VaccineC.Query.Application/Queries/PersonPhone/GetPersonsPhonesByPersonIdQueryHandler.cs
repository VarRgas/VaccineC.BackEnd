using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonPhone
{
    public class GetPersonsPhonesByPersonIdQueryHandler : IRequestHandler<GetPersonsPhonesByPersonIdQuery, IEnumerable<PersonPhoneViewModel>>
    {
        private readonly IPersonPhoneAppService _personPhoneAppService;

        public GetPersonsPhonesByPersonIdQueryHandler(IPersonPhoneAppService personPhoneAppService)
        {
            _personPhoneAppService = personPhoneAppService;
        }

        public async Task<IEnumerable<PersonPhoneViewModel>> Handle(GetPersonsPhonesByPersonIdQuery request, CancellationToken cancellationToken)
        {
            return await _personPhoneAppService.GetAllPersonsPhonesByPersonId(request.PersonID);
        }
    }
}
