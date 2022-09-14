using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.ProductDoses
{
    public class DeleteProductDosesCommand : IRequest<IEnumerable<ProductDosesViewModel>>
    {
        public Guid Id;

        public DeleteProductDosesCommand(Guid id)
        {
            Id = id;
        }
    }
}
