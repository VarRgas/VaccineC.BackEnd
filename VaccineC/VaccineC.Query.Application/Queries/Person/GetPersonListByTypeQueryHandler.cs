using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Person
{
    public class GetPersonListByTypeQueryHandler : IRequestHandler<GetPersonListByTypeQuery, IEnumerable<PersonViewModel>>
    {

        private readonly IPersonAppService _personAppService;

        public GetPersonListByTypeQueryHandler(IPersonAppService personAppService)
        {
            _personAppService = personAppService;
        }

        public async Task<IEnumerable<PersonViewModel>> Handle(GetPersonListByTypeQuery request, CancellationToken cancellationToken)
        {
            return await _personAppService.GetAllByType(request.PersonType);
        }
    }
}
