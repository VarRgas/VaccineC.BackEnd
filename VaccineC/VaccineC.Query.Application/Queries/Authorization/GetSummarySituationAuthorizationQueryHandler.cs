using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetSummarySituationAuthorizationQueryHandler : IRequestHandler<GetSummarySituationAuthorizationQuery, IEnumerable<AuthorizationSummarySituationViewModel>>
    {

        private readonly IAuthorizationAppService _appService;

        public GetSummarySituationAuthorizationQueryHandler(IAuthorizationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<AuthorizationSummarySituationViewModel>> Handle(GetSummarySituationAuthorizationQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetSummarySituationAuthorization();
        }
    }
}
