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

        public AddMovementProductCommandHandler(IMovementProductRepository movementProductRepository, IMovementProductAppService movementProductAppService)
        {
            _movementProductRepository = movementProductRepository;
            _movementProductAppService = movementProductAppService;
        }

        public async Task<IEnumerable<MovementProductViewModel>> Handle(AddMovementProductCommand request, CancellationToken cancellationToken)
        {

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
