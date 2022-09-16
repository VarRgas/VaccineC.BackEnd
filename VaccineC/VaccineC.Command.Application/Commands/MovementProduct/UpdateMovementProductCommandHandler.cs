using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.MovementProduct
{
    public class UpdateMovementProductCommandHandler : IRequestHandler<UpdateMovementProductCommand, IEnumerable<MovementProductViewModel>>
    {
        private readonly IMovementProductRepository _movementProductRepository;
        private readonly IMovementProductAppService _movementProductAppService;
        private readonly VaccineCCommandContext _ctx;

        public UpdateMovementProductCommandHandler(IMovementProductRepository movementProductRepository, IMovementProductAppService movementProductAppService, VaccineCCommandContext ctx)
        {
            _movementProductRepository = movementProductRepository;
            _movementProductAppService = movementProductAppService;
            _ctx = ctx;
        }

        public async Task<IEnumerable<MovementProductViewModel>> Handle(UpdateMovementProductCommand request, CancellationToken cancellationToken)
        {

            if (request.MovementType.Equals("S"))
            {

                var productSummaryBatch = _ctx.ProductsSummariesBatches
                 .Where(pmb => pmb.Batch.Equals(request.Batch) && pmb.ManufacturingDate.Equals(request.BatchManufacturingDate))
                 .FirstOrDefault();

                if (productSummaryBatch != null)
                {
                    if (productSummaryBatch.NumberOfUnitsBatch < request.UnitsNumber)
                    {
                        throw new ArgumentException("Não é possível retirar " + request.UnitsNumber + " unidades do lote " + productSummaryBatch.Batch + ", pois o total de unidades presentes é " + productSummaryBatch.NumberOfUnitsBatch);
                    }
                }
                else
                {
                    throw new ArgumentException("Lote não encontrado para o produto!");
                }

            }

            var movementProduct = _movementProductRepository.GetById(request.ID);
            movementProduct.SetProductId(request.ProductsId);
            movementProduct.SetBatch(request.Batch);
            movementProduct.SetUnitsNumber(request.UnitsNumber);
            movementProduct.SetUnitaryValue(request.UnitaryValue);
            movementProduct.SetAmount(request.Amount);
            movementProduct.SetDetails(request.Details);
            movementProduct.SetRegister(DateTime.Now);
            movementProduct.SetBatchExpirationDate(request.BatchExpirationDate);
            movementProduct.SetBatchManufacturingDate(request.BatchManufacturingDate);
            movementProduct.SetManufacturer(request.Manufacturer);

            await _movementProductRepository.SaveChangesAsync();

            return await _movementProductAppService.GetAllByMovementId(movementProduct.MovementId);

        }
    }
}
