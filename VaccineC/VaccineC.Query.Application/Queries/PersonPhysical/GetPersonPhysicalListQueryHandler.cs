using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonPhysical
{
    public class GetPersonPhysicalListQueryHandler : IRequestHandler<GetPersonPhysicalListQuery, IEnumerable<PersonsPhysicalViewModel>>
    {
        private readonly IPersonPhysicalAppService _personPhysicalAppService;

        public GetPersonPhysicalListQueryHandler(IPersonPhysicalAppService personPhysicalAppService)
        {
            _personPhysicalAppService = personPhysicalAppService;
        }

        public async Task<IEnumerable<PersonsPhysicalViewModel>> Handle(GetPersonPhysicalListQuery request, CancellationToken cancellationToken)
        {
            return await _personPhysicalAppService.GetAllAsync();
        }
    }
}
