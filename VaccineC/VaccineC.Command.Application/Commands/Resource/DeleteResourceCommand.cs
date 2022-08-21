using MediatR;

namespace VaccineC.Command.Application.Commands.Resource
{
    public class DeleteResourceCommand : IRequest
    {
        public Guid Id;

        public DeleteResourceCommand(Guid id)
        {
            Id = id;
        }
    }
}
