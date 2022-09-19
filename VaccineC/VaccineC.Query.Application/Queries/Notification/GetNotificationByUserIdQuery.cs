using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Notification
{
    public class GetNotificationByUserIdQuery : IRequest<IEnumerable<NotificationViewModel>>
    {
        public Guid UserId;

        public GetNotificationByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
