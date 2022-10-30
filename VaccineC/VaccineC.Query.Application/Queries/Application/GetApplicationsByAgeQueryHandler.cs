using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationsByAgeQueryHandler : IRequestHandler<GetApplicationsByAgeQuery, IEnumerable<ApplicationPersonAgeViewModel>>
    {
        private readonly IApplicationAppService _appService;

        public GetApplicationsByAgeQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ApplicationPersonAgeViewModel>> Handle(GetApplicationsByAgeQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetApplicationsByPersonAge(request.Month, request.Year);
        }
    }
}
