using MediatR;

namespace VaccineC.Command.Application.Commands.ProductSummaryBatch
{
    public class DeleteProductSummaryBatchCommand : IRequest
    {
        public Guid Id;

        public DeleteProductSummaryBatchCommand(Guid id)
        {
            Id = id;
        }
    }
}
