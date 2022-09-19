using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Notification
{
    public class GetNotificationByUserIdQueryHandler : IRequestHandler<GetNotificationByUserIdQuery, IEnumerable<NotificationViewModel>>
    {

        private readonly INotificationAppService _appService;

        public GetNotificationByUserIdQueryHandler(INotificationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<NotificationViewModel>> Handle(GetNotificationByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllByUser(request.UserId);
        }
    }
}
