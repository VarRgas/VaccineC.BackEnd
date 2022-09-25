using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetNegotiation
{
    public class DeleteBudgetNegotiationCommandHandler : IRequestHandler<DeleteBudgetNegotiationCommand, IEnumerable<BudgetNegotiationViewModel>>
    {
        private readonly IBudgetNegotiationRepository _repository;
        private readonly IBudgetNegotiationAppService _appService;

        public DeleteBudgetNegotiationCommandHandler(IBudgetNegotiationRepository repository, IBudgetNegotiationAppService appService)
        {
            _repository = repository;
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetNegotiationViewModel>> Handle(DeleteBudgetNegotiationCommand request, CancellationToken cancellationToken)
        {

            var budgetNegotiation = _repository.GetById(request.Id);
            _repository.Remove(budgetNegotiation);

            await _repository.SaveChangesAsync();

            return await _appService.GetAllBudgetsNegotiationsByBudgetId(budgetNegotiation.BudgetId);

        }
    }
}
