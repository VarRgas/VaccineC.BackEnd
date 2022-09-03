using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonPhone
{
    public class GetPersonPhoneListQueryHandler : IRequestHandler<GetPersonPhoneListQuery, IEnumerable<PersonPhoneViewModel>>
    {
        private readonly IPersonPhoneAppService _personPhoneAppService;

        public GetPersonPhoneListQueryHandler(IPersonPhoneAppService personPhoneAppService)
        {
            _personPhoneAppService = personPhoneAppService;
        }

        public async Task<IEnumerable<PersonPhoneViewModel>> Handle(GetPersonPhoneListQuery request, CancellationToken cancellationToken)
        {
            return await _personPhoneAppService.GetAllAsync();
        }
    }
}
