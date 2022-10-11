using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.AuthorizationNotification
{
    public class GetAuthorizationNotificationListQueryHandler : IRequestHandler<GetAuthorizationNotificationListQuery, IEnumerable<AuthorizationNotificationViewModel>>
    {

        private readonly IAuthorizationNotificationAppService _appService;

        public GetAuthorizationNotificationListQueryHandler(IAuthorizationNotificationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<AuthorizationNotificationViewModel>> Handle(GetAuthorizationNotificationListQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllAsync();
        }
    }
}
