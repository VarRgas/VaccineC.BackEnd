using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Notification
{
    public class DeleteNotificationCommand : IRequest<IEnumerable<NotificationViewModel>>
    {
        public Guid Id;

        public DeleteNotificationCommand(Guid id)
        {
            Id = id;
        }
    }
}
