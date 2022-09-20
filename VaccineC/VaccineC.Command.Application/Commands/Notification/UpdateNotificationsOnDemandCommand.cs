using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Notification
{
    public class UpdateNotificationsOnDemandCommand : IRequest<IEnumerable<NotificationViewModel>>
    {
        public List<NotificationViewModel> ListNotificationViewModel;

        public UpdateNotificationsOnDemandCommand(List<NotificationViewModel> listNotificationViewModel)
        {
            ListNotificationViewModel = listNotificationViewModel;
        }
    }
}
