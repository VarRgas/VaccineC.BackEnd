using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.AuthorizationNotification
{
    public class AddAuthorizationNotificationCommand : IRequest
    {
        public Guid ID;
        public Guid AuthorizationId;
        public Guid EventId;
        public string PersonPhone;
        public string Message;
        public DateTime SendDate;
        public TimeSpan SendHour;
        public DateTime Register;
        public string? ReturnId;

        public AddAuthorizationNotificationCommand(Guid id, Guid authorizationId, Guid eventId, string personPhone, string message, DateTime sendDate, TimeSpan sendHour, DateTime register, string? returnId)
        {
            ID = id;
            AuthorizationId = authorizationId;
            EventId = eventId;
            PersonPhone = personPhone;
            Message = message;
            SendDate = sendDate;
            SendHour = sendHour;
            Register = register;
            ReturnId = returnId;
        }
    }
}
