using MediatR;

namespace VaccineC.Command.Application.Commands.ProductDoses
{
    public class DeleteProductDosesCommand : IRequest
    {
        public Guid Id;

        public DeleteProductDosesCommand(Guid id)
        {
            Id = id;
        }
    }
}
