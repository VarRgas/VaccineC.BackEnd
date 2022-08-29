using MediatR;

namespace VaccineC.Command.Application.Commands.UserResource
{
    public class DeleteUserResourceCommand : IRequest
    {
        public Guid Id;

        public DeleteUserResourceCommand(Guid id)
        {
            Id = id;
        }
    }
}