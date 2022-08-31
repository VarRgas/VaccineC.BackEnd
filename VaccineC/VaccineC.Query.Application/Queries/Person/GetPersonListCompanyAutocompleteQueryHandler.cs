using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Person
{
    public class GetPersonListCompanyAutocompleteQueryHandler : IRequestHandler<GetPersonListCompanyAutocompleteQuery, IEnumerable<PersonViewModel>>
    {
        private readonly IPersonAppService _personAppService;

        public GetPersonListCompanyAutocompleteQueryHandler(IPersonAppService personAppService)
        {
            _personAppService = personAppService;
        }

        public async Task<IEnumerable<PersonViewModel>> Handle(GetPersonListCompanyAutocompleteQuery request, CancellationToken cancellationToken)
        {
            return await _personAppService.GetAllCompanyAutocomplete();
        }
    }
}
