using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.ProductSummaryBatch
{
    public class DeleteProductSummaryBatchCommandHandler : IRequestHandler<DeleteProductSummaryBatchCommand, Unit>
    {
        private readonly IProductSummaryBatchRepository _repository;

        public DeleteProductSummaryBatchCommandHandler(IProductSummaryBatchRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProductSummaryBatchCommand request, CancellationToken cancellationToken)
        {
            var productSummaryBatch = _repository.GetById(request.Id);

            if (productSummaryBatch == null)
            {
                throw new ArgumentException("Produto Lote não encontrado!");
            }

            _repository.Remove(productSummaryBatch);
            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
