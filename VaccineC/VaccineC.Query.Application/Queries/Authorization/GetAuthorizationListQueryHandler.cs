using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetAuthorizationListQueryHandler : IRequestHandler<GetAuthorizationListQuery, IEnumerable<AuthorizationViewModel>>
    {

        private readonly IAuthorizationAppService _appService;

        public GetAuthorizationListQueryHandler(IAuthorizationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<AuthorizationViewModel>> Handle(GetAuthorizationListQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllAsync();
        }
    }
}
