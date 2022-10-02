using MediatR;
using VaccineC.Command.Application.Commands.BudgetHistoric;
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
        private readonly IMediator _mediator;

        public DeleteBudgetNegotiationCommandHandler(IBudgetNegotiationRepository repository, IBudgetNegotiationAppService appService, IMediator mediator)
        {
            _repository = repository;
            _appService = appService;
            _mediator = mediator;   
        }

        public async Task<IEnumerable<BudgetNegotiationViewModel>> Handle(DeleteBudgetNegotiationCommand request, CancellationToken cancellationToken)
        {

            var budgetNegotiation = _repository.GetById(request.Id);
            await addNewBudgetHistoric(budgetNegotiation, request.UserId);
            _repository.Remove(budgetNegotiation);

            await _repository.SaveChangesAsync();

            return await _appService.GetAllBudgetsNegotiationsByBudgetId(budgetNegotiation.BudgetId);

        }

        public async Task<Unit> addNewBudgetHistoric(Domain.Entities.BudgetNegotiation budgetNegotiation, Guid? userId)
        {
            var budgetProductViewModel = _appService.GetById(budgetNegotiation.ID);

            string historic = "Negociação removida do orçamento: " + budgetProductViewModel.Installments + "x de R$" + budgetProductViewModel.TotalAmountTraded + " - " + budgetProductViewModel.PaymentForm.Name + ".";

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
