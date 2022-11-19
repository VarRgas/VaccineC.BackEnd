using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationSipniIntegrationQueryHandler : IRequestHandler<GetApplicationSipniIntegrationQuery, IEnumerable<ApplicationSipniViewModel>>
    {

        private readonly IApplicationAppService _appService;

        public GetApplicationSipniIntegrationQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ApplicationSipniViewModel>> Handle(GetApplicationSipniIntegrationQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetApplicationSipniIntegration();
        }
    }
}
