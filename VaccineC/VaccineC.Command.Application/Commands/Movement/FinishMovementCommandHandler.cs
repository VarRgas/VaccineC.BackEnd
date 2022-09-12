using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.Movement
{
    public class FinishMovementCommandHandler : IRequestHandler<FinishMovementCommand, MovementViewModel>
    {
        private readonly IMovementRepository _movementRepository;
        private readonly VaccineCCommandContext _ctx;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public FinishMovementCommandHandler(IMovementRepository movementRepository, VaccineCCommandContext ctx, IQueryContext queryContext, IMapper mapper)
        {
            _movementRepository = movementRepository;
            _ctx = ctx;
            _mapper = mapper;
            _queryContext = queryContext;
        }

        public async Task<MovementViewModel> Handle(FinishMovementCommand request, CancellationToken cancellationToken)
        {

            var movement = _movementRepository.GetById(request.ID);

            await this.validateMovementProduct(movement);

            movement.SetSituation("F");
            movement.SetUsersId(request.UsersId);
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

        private async Task<Boolean> validateMovementProduct(Domain.Entities.Movement movement)
        {
            var movementsProducts = await _queryContext.AllMovementsProducts.ToListAsync();
            var movementsProductsViewModel = movementsProducts
                .Select(r => _mapper.Map<MovementProductViewModel>(r))
                .Where(r => r.MovementId == movement.ID)
                .ToList();

            if (movementsProductsViewModel.Count == 0)
            {
                throw new ArgumentException("Não é possível finalizar um movimento sem Produtos!");
            }
            else
            {
                return true;
            }
        }

    }
}
