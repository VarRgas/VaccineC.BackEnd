using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.MovementProduct
{
    public class DeleteMovementProductCommandHandler : IRequestHandler<DeleteMovementProductCommand, IEnumerable<MovementProductViewModel>>
    {
        private readonly IMovementProductRepository _movementProductRepository;
        private readonly IMovementProductAppService _movementProductAppService;

        public DeleteMovementProductCommandHandler(IMovementProductRepository movementProductRepository, IMovementProductAppService movementProductAppService)
        {
            _movementProductRepository = movementProductRepository;
            _movementProductAppService = movementProductAppService;
        }

        public async Task<IEnumerable<MovementProductViewModel>> Handle(DeleteMovementProductCommand request, CancellationToken cancellationToken)
        {
            var movementProduct = _movementProductRepository.GetById(request.Id);

            if (movementProduct == null)
            {
                throw new ArgumentException("Movimento Produto não encontrado!");
            }

            _movementProductRepository.Remove(movementProduct);

            await _movementProductRepository.SaveChangesAsync();

            return await _movementProductAppService.GetAllByMovementId(movementProduct.MovementId);

        }
    }
}
