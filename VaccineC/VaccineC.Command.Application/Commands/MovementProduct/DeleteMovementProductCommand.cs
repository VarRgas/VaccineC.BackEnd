using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.MovementProduct
{
    public class DeleteMovementProductCommand : IRequest<IEnumerable<MovementProductViewModel>>
    {
        public Guid Id;
        public DeleteMovementProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
