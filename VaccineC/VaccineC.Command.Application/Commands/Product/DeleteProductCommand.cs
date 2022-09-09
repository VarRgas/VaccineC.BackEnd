using MediatR;

namespace VaccineC.Command.Application.Commands.Product
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id;

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
