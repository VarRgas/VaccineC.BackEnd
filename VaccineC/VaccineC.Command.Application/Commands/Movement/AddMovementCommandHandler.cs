using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Movement
{
    public class AddMovementCommandHandler : IRequestHandler<AddMovementCommand, MovementViewModel>
    {
        private readonly IMovementRepository _movementRepository;
        private readonly VaccineCCommandContext _ctx;

        public AddMovementCommandHandler(IMovementRepository movementRepository, VaccineCCommandContext ctx)
        {
            _movementRepository = movementRepository;
            _ctx = ctx;
        }

        public async Task<MovementViewModel> Handle(AddMovementCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.Movement newMovement = new Domain.Entities.Movement(
                Guid.NewGuid(),
                request.UsersId,
                request.MovementType,
                request.ProductsAmount,
                DateTime.Now,
                "P"
                );

            _movementRepository.Add(newMovement);
            await _movementRepository.SaveChangesAsync();

            return new MovementViewModel()
            {
                ID = newMovement.ID,
                MovementNumber = newMovement.MovementNumber,
                UsersId = newMovement.UsersId,
                MovementType = newMovement.MovementType,
                ProductsAmount = newMovement.ProductsAmount,
                Situation = newMovement.Situation
            };
        }
    }
}
