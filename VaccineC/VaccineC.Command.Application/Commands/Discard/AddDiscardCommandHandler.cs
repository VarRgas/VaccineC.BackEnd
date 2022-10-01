using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Discard
{
    public class AddDiscardCommandHandler : IRequestHandler<AddDiscardCommand, Unit>
    {
        private readonly IDiscardRepository _discardRepository;
        private readonly IProductSummaryBatchRepository _productSummaryBatchrepository;
        private readonly VaccineCCommandContext _ctx;

        public AddDiscardCommandHandler(IDiscardRepository discardRepository, IProductSummaryBatchRepository productSummaryBatchrepository, VaccineCCommandContext ctx)
        {
            _discardRepository = discardRepository;
            _productSummaryBatchrepository = productSummaryBatchrepository;
            _ctx = ctx;
        }

        public async Task<Unit> Handle(AddDiscardCommand request, CancellationToken cancellationToken)
        {

            var productSummaryBatch = _productSummaryBatchrepository.GetById(request.ProductSummaryBatchId);

            if (productSummaryBatch == null)
            {
                throw new ArgumentException("Produto Lote não encontrado!");
            }

            productSummaryBatch.SetNumberOfUnitsBatch(0);
            await _productSummaryBatchrepository.SaveChangesAsync();

            Domain.Entities.Discard newDiscard = new Domain.Entities.Discard(
                Guid.NewGuid(), 
                request.ProductSummaryBatchId, 
                request.UserId,
                request.Batch,
                request.DiscardedUnits,
                DateTime.Now);

            _discardRepository.Add(newDiscard);
            await _discardRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
