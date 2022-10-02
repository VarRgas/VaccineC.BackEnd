using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetHistoric
{
    public class AddBudgetHistoricCommandHandler : IRequestHandler<AddBudgetHistoricCommand, Unit>
    {
        private readonly IBudgetHistoricRepository _repository;
        private readonly IBudgetHistoricAppService _appService;

        public AddBudgetHistoricCommandHandler(IBudgetHistoricRepository repository, IBudgetHistoricAppService appService)
        {
            _repository = repository;
            _appService = appService;
        }

        public async Task<Unit> Handle(AddBudgetHistoricCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.BudgetHistoric newBudgetHistoric = new Domain.Entities.BudgetHistoric(
                Guid.NewGuid(),
                request.BudgetId,
                request.UserId,
                request.Historic,
                DateTime.Now
                );

            _repository.Add(newBudgetHistoric);
            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
