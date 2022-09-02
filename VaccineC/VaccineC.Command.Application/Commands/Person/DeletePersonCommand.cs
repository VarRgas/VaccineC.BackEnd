using MediatR;

namespace VaccineC.Command.Application.Commands.Person
{
    public class DeletePersonCommand : IRequest
    {
        public Guid Id;

        public DeletePersonCommand(Guid id)
        {
            Id = id;
        }
    }
}
