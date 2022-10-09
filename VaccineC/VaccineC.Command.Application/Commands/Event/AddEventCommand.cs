using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Event
{
    public class AddEventCommand : IRequest
    {
        public Guid ID;
        public Guid UserId;
        public string Situation;
        public string Concluded;
        public DateTime StartDate;
        public DateTime EndDate;
        public TimeSpan StartTime;
        public TimeSpan EndTime;
        public string? Details;
        public DateTime Register;

        public AddEventCommand(Guid id, Guid userId, string situation, string concluded, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime, string? details, DateTime register)
        {
            ID = id;
            UserId = userId;
            Situation = situation;
            Concluded = concluded;
            StartDate = startDate;
            EndDate = endDate;
            StartTime = startTime;
            EndTime = endTime;
            Details = details;
            Register = register;
        }
    }
}
