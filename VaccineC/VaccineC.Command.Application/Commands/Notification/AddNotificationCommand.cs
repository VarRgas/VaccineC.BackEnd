using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Notification
{
    public class AddNotificationCommand : IRequest<NotificationViewModel>
    {
        public Guid ID;
        public Guid UserId;
        public string Message;
        public string MessageType;
        public string Situation;
        public DateTime Register;

        public AddNotificationCommand(Guid id, Guid userId, string message, string messageType, string situation, DateTime register)
        {
            ID = id;
            UserId = userId;
            Message = message;
            MessageType = messageType;
            Situation = situation;
            Register = register;
        }
    }
}
