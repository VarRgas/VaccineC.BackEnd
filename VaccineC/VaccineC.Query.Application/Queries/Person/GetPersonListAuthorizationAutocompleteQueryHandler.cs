using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Person
{
    public class GetPersonListAuthorizationAutocompleteQueryHandler : IRequestHandler<GetPersonListAuthorizationAutocompleteQuery, IEnumerable<PersonViewModel>>
    {
        private readonly IPersonAppService _personAppService;

        public GetPersonListAuthorizationAutocompleteQueryHandler(IPersonAppService personAppService)
        {
            _personAppService = personAppService;
        }

        public async Task<IEnumerable<PersonViewModel>> Handle(GetPersonListAuthorizationAutocompleteQuery request, CancellationToken cancellationToken)
        {
            return await _personAppService.GetAllAuthorizationAutocomplete();
        }
    }
}
