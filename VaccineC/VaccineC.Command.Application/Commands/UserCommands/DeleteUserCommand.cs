using MediatR;

namespace VaccineC.Command.Application.Commands.UserCommands
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id;

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
