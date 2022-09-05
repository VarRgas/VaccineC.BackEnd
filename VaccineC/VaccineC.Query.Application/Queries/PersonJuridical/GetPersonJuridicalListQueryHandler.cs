using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PersonJuridical
{
    public class GetPersonJuridicalListQueryHandler : IRequestHandler<GetPersonJuridicalListQuery, IEnumerable<PersonsJuridicalViewModel>>
    {
        private readonly IPersonJuridicalAppService _personJuridicalAppService;

        public GetPersonJuridicalListQueryHandler(IPersonJuridicalAppService personJuridicalAppService)
        {
            _personJuridicalAppService = personJuridicalAppService;
        }

        public async Task<IEnumerable<PersonsJuridicalViewModel>> Handle(GetPersonJuridicalListQuery request, CancellationToken cancellationToken)
        {
            return await _personJuridicalAppService.GetAllAsync();
        }
    }
}
