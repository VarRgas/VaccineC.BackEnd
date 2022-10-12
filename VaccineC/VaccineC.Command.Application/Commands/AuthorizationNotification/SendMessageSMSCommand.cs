using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.AuthorizationNotification
{
    public class SendMessageSMSCommand : IRequest
    {
        public Guid AuthorizationNotificationId;
        public string Number;
        public string Message;
        public string JobDate;
        public string JobTime;

        public SendMessageSMSCommand(Guid authorizationNotificationId, string number, string message, string jobDate, string jobTime)
        {
            AuthorizationNotificationId = authorizationNotificationId;
            Number = number;
            Message = message;
            JobDate = jobDate;
            JobTime = jobTime;
        }
    }
}
