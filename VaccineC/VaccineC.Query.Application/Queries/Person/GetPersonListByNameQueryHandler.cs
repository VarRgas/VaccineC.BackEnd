using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Person
{
    public class GetPersonListByNameQueryHandler : IRequestHandler<GetPersonListByNameQuery, IEnumerable<PersonViewModel>>
    {
        private readonly IPersonAppService _personAppService;

        public GetPersonListByNameQueryHandler(IPersonAppService personAppService)
        {
            _personAppService = personAppService;
        }

        public async Task<IEnumerable<PersonViewModel>> Handle(GetPersonListByNameQuery request, CancellationToken cancellationToken)
        {
            return await _personAppService.GetByName(request.Name);
        }

    }
}
