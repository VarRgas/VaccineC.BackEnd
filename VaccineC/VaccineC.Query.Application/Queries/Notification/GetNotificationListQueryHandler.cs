using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.Notification
{
    public class GetNotificationListQueryHandler : IRequestHandler<GetNotificationListQuery, IEnumerable<NotificationViewModel>>
    {

        private readonly INotificationAppService _appService;

        public GetNotificationListQueryHandler(INotificationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<NotificationViewModel>> Handle(GetNotificationListQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllAsync();
        }
    }
}
