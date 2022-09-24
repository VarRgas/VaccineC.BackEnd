using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Budget
{
    public class AddBudgetCommandHandler : IRequestHandler<AddBudgetCommand, BudgetViewModel>
    {
        private readonly IBudgetRepository _repository;
        private readonly VaccineCCommandContext _ctx;
        private readonly IBudgetAppService _appService;

        public AddBudgetCommandHandler(IBudgetRepository repository, VaccineCCommandContext ctx, IBudgetAppService appService)
        {
            _repository = repository;
            _ctx = ctx;
            _appService = appService;
        }

        public async Task<BudgetViewModel> Handle(AddBudgetCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.Budget newBudget = new Domain.Entities.Budget(
                Guid.NewGuid(),
                request.UserID,
                request.PersonID,
                "P",
                0,
                0,
                request.TotalBudgetAmount,
                request.TotalBudgetedAmount,
                request.ExpirationDate,
                request.ApprovalDate,
                request.Details,
                request.BudgetNumber,
                DateTime.Now
                );

            _repository.Add(newBudget);
            await _repository.SaveChangesAsync();

            return await _appService.GetById(newBudget.ID);

        }
    }
}
