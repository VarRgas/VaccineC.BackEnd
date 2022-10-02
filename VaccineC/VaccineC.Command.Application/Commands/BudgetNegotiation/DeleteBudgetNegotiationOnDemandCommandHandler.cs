using MediatR;
using VaccineC.Command.Application.Commands.BudgetHistoric;
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
        private readonly IMediator _mediator;

        public DeleteBudgetNegotiationOnDemandCommandHandler(IBudgetNegotiationRepository repository, IBudgetNegotiationAppService appService, IMediator mediator)
        {
            _repository = repository;
            _appService = appService;
            _mediator = mediator;   
        }

        public async Task<IEnumerable<BudgetNegotiationViewModel>> Handle(DeleteBudgetNegotiationOnDemandCommand request, CancellationToken cancellationToken)
        {

            List<BudgetNegotiationViewModel> listBudgetNegotiationViewModel = request.ListBudgetNegotiationViewModel;

            await addNewBudgetHistoric(listBudgetNegotiationViewModel[0], request.userId);

            foreach (BudgetNegotiationViewModel budgetNegotiationViewModel in listBudgetNegotiationViewModel)
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

        public async Task<Unit> addNewBudgetHistoric(BudgetNegotiationViewModel budgetNegotiation, Guid? userId)
        {
            var budgetProductViewModel = _appService.GetById(budgetNegotiation.ID);

            string historic = "Negociações removidas do orçamento.";

            await _mediator.Send(new AddBudgetHistoricCommand(
                 Guid.NewGuid(),
                 budgetProductViewModel.BudgetId,
                 userId,
                 historic,
                 DateTime.Now
                 ));

            return Unit.Value;
        }
    }
}
