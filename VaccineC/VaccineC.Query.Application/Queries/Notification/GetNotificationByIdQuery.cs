using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Notification
{
    public class GetNotificationByIdQuery : IRequest<NotificationViewModel>
    {
        public Guid Id;

        public GetNotificationByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
