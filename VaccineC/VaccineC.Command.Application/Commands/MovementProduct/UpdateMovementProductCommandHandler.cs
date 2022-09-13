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

        public UpdateMovementProductCommandHandler(IMovementProductRepository movementProductRepository, IMovementProductAppService movementProductAppService)
        {
            _movementProductRepository = movementProductRepository;
            _movementProductAppService = movementProductAppService;
        }

        public async Task<IEnumerable<MovementProductViewModel>> Handle(UpdateMovementProductCommand request, CancellationToken cancellationToken)
        {
            var movementProduct = _movementProductRepository.GetById(request.ID);
            movementProduct.SetProductId(request.ProductId);
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
