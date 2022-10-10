using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class UpdateAuthorizationCommand : IRequest<IEnumerable<AuthorizationViewModel>>
    {
        public Guid ID;
        public Guid UserId;
        public Guid EventId;
        public DateTime Register;
        public DateTime StartDateEvent;
        public TimeSpan StartTimeEvent;

        public UpdateAuthorizationCommand(Guid id, Guid userId, Guid eventId, DateTime register, DateTime startDateEvent, TimeSpan startTimeEvent) {
            ID = id;
            UserId = userId;
            EventId = eventId;
            Register = register;
            StartDateEvent = startDateEvent;
            StartTimeEvent = startTimeEvent;
        }
    }
}
