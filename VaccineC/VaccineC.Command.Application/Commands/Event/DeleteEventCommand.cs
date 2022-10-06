using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Event
{
    public class DeleteEventCommand : IRequest
    {
        public Guid Id;

        public DeleteEventCommand(Guid id)
        {
            Id = id;
        }
    }
}
