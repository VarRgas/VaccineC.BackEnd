using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetAvailableApplicationsByPersonIdQueryHandler : IRequestHandler<GetAvailableApplicationsByPersonIdQuery, IEnumerable<ApplicationAvailableViewModel>>
    {

        private readonly IApplicationAppService _appService;

        public GetAvailableApplicationsByPersonIdQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ApplicationAvailableViewModel>> Handle(GetAvailableApplicationsByPersonIdQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAvailableApplicationsByPersonId(request.PersonId);
        }
    }
}
