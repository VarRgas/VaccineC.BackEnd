using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Notification
{
    public class GetNotificationListQuery : IRequest<IEnumerable<NotificationViewModel>>
    {
    }
}
