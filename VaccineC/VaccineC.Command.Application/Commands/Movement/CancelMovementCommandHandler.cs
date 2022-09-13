using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Movement
{
    public class CancelMovementCommandHandler : IRequestHandler<CancelMovementCommand, MovementViewModel>
    {
        private readonly IMovementRepository _movementRepository;
        private readonly VaccineCCommandContext _ctx;

        public CancelMovementCommandHandler(IMovementRepository movementRepository, VaccineCCommandContext ctx)
        {
            _movementRepository = movementRepository;
            _ctx = ctx;
        }

        public async Task<MovementViewModel> Handle(CancelMovementCommand request, CancellationToken cancellationToken)
        {
            var movement = _movementRepository.GetById(request.ID);
            movement.SetSituation("C");
            movement.SetUsersId(request.UsersId);
            movement.SetProductsAmount(request.ProductsAmount);
            movement.SetRegister(DateTime.Now);

            await _movementRepository.SaveChangesAsync();

            return new MovementViewModel()
            {
                ID = movement.ID,
                MovementNumber = movement.MovementNumber,
                UsersId = movement.UsersId,
                MovementType = movement.MovementType,
                ProductsAmount = movement.ProductsAmount,
                Situation = movement.Situation
            };
        }
    }
}
