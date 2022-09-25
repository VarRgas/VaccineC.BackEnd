using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetNegotiation
{
    public class AddBudgetNegotiationCommandHandler : IRequestHandler<AddBudgetNegotiationCommand, IEnumerable<BudgetNegotiationViewModel>>
    {
        private readonly IBudgetNegotiationRepository _repository;
        private readonly IBudgetNegotiationAppService _appService;
        private readonly VaccineCCommandContext _ctx;

        public AddBudgetNegotiationCommandHandler(IBudgetNegotiationRepository repository, IBudgetNegotiationAppService appService, VaccineCCommandContext ctx)
        {
            _repository = repository;
            _appService = appService;
            _ctx = ctx;
        }

        public async Task<IEnumerable<BudgetNegotiationViewModel>> Handle(AddBudgetNegotiationCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.BudgetNegotiation newBudgetNegotiation = new Domain.Entities.BudgetNegotiation(
                Guid.NewGuid(),
                request.BudgetId,
                request.PaymentFormId,
                request.TotalAmountBalance,
                request.TotalAmountTraded,
                request.Installments,
                DateTime.Now
                );

            _repository.Add(newBudgetNegotiation);
            await _repository.SaveChangesAsync();

            return await _appService.GetAllBudgetsNegotiationsByBudgetId(newBudgetNegotiation.BudgetId);
        }
    }
}
