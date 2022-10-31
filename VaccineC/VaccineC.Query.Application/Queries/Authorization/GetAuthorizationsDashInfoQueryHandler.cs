using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetAuthorizationsDashInfoQueryHandler : IRequestHandler<GetAuthorizationsDashInfoQuery, AuthorizationDashInfoViewModel>
    {

        private readonly IAuthorizationAppService _appService;

        public GetAuthorizationsDashInfoQueryHandler(IAuthorizationAppService appService)
        {
            _appService = appService;
        }

        public async Task<AuthorizationDashInfoViewModel> Handle(GetAuthorizationsDashInfoQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAuthorizationDashInfo(request.Month, request.Year);
        }
    }
}
