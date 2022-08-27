using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.Person
{
    public class GetPersonListQueryHandler : IRequestHandler<GetPersonListQuery, IEnumerable<PersonViewModel>>
    {

        private readonly IPersonAppService _personAppService;

        public GetPersonListQueryHandler(IPersonAppService personAppService)
        {
            _personAppService = personAppService;
        }

        public async Task<IEnumerable<PersonViewModel>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            return await _personAppService.GetAllAsync();
        }
    }
}
