using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Person
{
    public class GetPersonListUserAutocompleteQueryHandler : IRequestHandler<GetPersonListUserAutocompleteQuery, IEnumerable<PersonViewModel>>
    {

        private readonly IPersonAppService _personAppService;

        public GetPersonListUserAutocompleteQueryHandler(IPersonAppService personAppService)
        {
            _personAppService = personAppService;
        }

        public async Task<IEnumerable<PersonViewModel>> Handle(GetPersonListUserAutocompleteQuery request, CancellationToken cancellationToken)
        {
            return await _personAppService.GetAllUserAutocomplete();
        }
    }
}
