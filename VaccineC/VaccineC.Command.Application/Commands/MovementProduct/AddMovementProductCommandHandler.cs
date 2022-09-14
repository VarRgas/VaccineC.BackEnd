using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.MovementProduct
{
    public class AddMovementProductCommandHandler : IRequestHandler<AddMovementProductCommand, IEnumerable<MovementProductViewModel>>
    {
        private readonly IMovementProductRepository _movementProductRepository;
        private readonly IMovementProductAppService _movementProductAppService;
        private readonly VaccineCCommandContext _ctx;

        public AddMovementProductCommandHandler(IMovementProductRepository movementProductRepository, IMovementProductAppService movementProductAppService, VaccineCCommandContext ctx)
        {
            _movementProductRepository = movementProductRepository;
            _movementProductAppService = movementProductAppService;
            _ctx = ctx;
        }

        public async Task<IEnumerable<MovementProductViewModel>> Handle(AddMovementProductCommand request, CancellationToken cancellationToken)
        {

            if (request.MovementType.Equals("S")) {

                var productSummaryBatch = _ctx.ProductsSummariesBatches
                 .Where(pmb => pmb.Batch.Equals(request.Batch) && pmb.ManufacturingDate.Equals(request.BatchManufacturingDate))
                 .FirstOrDefault();

                if (productSummaryBatch != null) {
                    if (productSummaryBatch.NumberOfUnitsBatch < request.UnitsNumber) {
                        throw new ArgumentException("Não é possível retirar " + request.UnitsNumber + " unidades do lote " + productSummaryBatch.Batch + ", pois o total de unidades presentes é " + productSummaryBatch.NumberOfUnitsBatch);
                    }
                }
                else
                {
                    throw new ArgumentException("Lote não encontrado para o produto!");
                }

            }

            Domain.Entities.MovementProduct newMovementProduct = new Domain.Entities.MovementProduct(
                Guid.NewGuid(),
                request.MovementId,
                request.ProductId,
                request.Batch,
                request.UnitsNumber,
                request.UnitaryValue,
                request.Amount,
                request.Details,
                DateTime.Now,
                request.BatchManufacturingDate,
                request.BatchExpirationDate,
                request.Manufacturer
                );

            _movementProductRepository.Add(newMovementProduct);
            await _movementProductRepository.SaveChangesAsync();

            return await _movementProductAppService.GetAllByMovementId(newMovementProduct.MovementId);
        }
    }
}
