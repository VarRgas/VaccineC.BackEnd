using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationSipniIntegrationByParameterQueryHandler : IRequestHandler<GetApplicationSipniIntegrationByParameterQuery, IEnumerable<ApplicationSipniViewModel>>
    {

        private readonly IApplicationAppService _appService;

        public GetApplicationSipniIntegrationByParameterQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ApplicationSipniViewModel>> Handle(GetApplicationSipniIntegrationByParameterQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetApplicationSipniIntegrationByParameter(request.Borrower, request.Situation);
        }
    }
}
