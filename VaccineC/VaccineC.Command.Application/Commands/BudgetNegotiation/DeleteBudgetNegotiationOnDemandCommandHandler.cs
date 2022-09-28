using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetNegotiation
{
    public class DeleteBudgetNegotiationOnDemandCommandHandler : IRequestHandler<DeleteBudgetNegotiationOnDemandCommand, IEnumerable<BudgetNegotiationViewModel>>
    {
        private readonly IBudgetNegotiationRepository _repository;
        private readonly IBudgetNegotiationAppService _appService;

        public DeleteBudgetNegotiationOnDemandCommandHandler(IBudgetNegotiationRepository repository, IBudgetNegotiationAppService appService)
        {
            _repository = repository;
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetNegotiationViewModel>> Handle(DeleteBudgetNegotiationOnDemandCommand request, CancellationToken cancellationToken)
        {

            List<BudgetNegotiationViewModel> listBudgetNegotiationViewModel = request.ListBudgetNegotiationViewModel;

            foreach(BudgetNegotiationViewModel budgetNegotiationViewModel in listBudgetNegotiationViewModel)
            {
                var budgetNegotiation = _repository.GetById(budgetNegotiationViewModel.ID);

                if(budgetNegotiation == null)
                {
                    throw new ArgumentException("Negociação não encontrada!");
                }

                _repository.Remove(budgetNegotiation);

                await _repository.SaveChangesAsync();
            }

            return await _appService.GetAllBudgetsNegotiationsByBudgetId(listBudgetNegotiationViewModel[0].BudgetId);
        }
    }
}
