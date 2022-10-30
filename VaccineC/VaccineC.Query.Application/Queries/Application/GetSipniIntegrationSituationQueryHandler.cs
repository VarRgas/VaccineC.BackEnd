using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetSipniIntegrationSituationQueryHandler : IRequestHandler<GetSipniIntegrationSituationQuery, IEnumerable<ApplicationSipniIntegrationViewModel>>
    {
        private readonly IApplicationAppService _appService;

        public GetSipniIntegrationSituationQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ApplicationSipniIntegrationViewModel>> Handle(GetSipniIntegrationSituationQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetSipniIntegrationSituation(request.Month, request.Year);
        }
    }
}
