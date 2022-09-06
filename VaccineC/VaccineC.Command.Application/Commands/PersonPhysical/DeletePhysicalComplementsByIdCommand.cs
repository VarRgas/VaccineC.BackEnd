using MediatR;

namespace VaccineC.Command.Application.Commands.PersonPhysical
{
    public class DeletePhysicalComplementsByIdCommand : IRequest
    {
        public Guid Id;

        public DeletePhysicalComplementsByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
