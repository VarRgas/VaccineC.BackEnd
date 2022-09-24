using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Budget
{
    public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, BudgetViewModel>
    {
        private readonly IBudgetRepository _repository;
        private readonly VaccineCCommandContext _ctx;
        private readonly IBudgetAppService _appService;

        public UpdateBudgetCommandHandler(IBudgetRepository repository, VaccineCCommandContext ctx, IBudgetAppService appService)
        {
            _repository = repository;
            _ctx = ctx;
            _appService = appService;
        }

        public async Task<BudgetViewModel> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
        {

            var updatedBudget = _repository.GetById(request.ID);
            updatedBudget.SetSituation(request.Situation);
            updatedBudget.SetTotalBudgetAmount(request.TotalBudgetAmount);
            updatedBudget.SetTotalBudgetedAmount(request.TotalBudgetedAmount);
            updatedBudget.SetDiscountPercentage(request.DiscountPercentage);
            updatedBudget.SetDiscountValue(request.DiscountValue);
            updatedBudget.SetExpirationDate(request.ExpirationDate);
            updatedBudget.SetApprovalDate(request.ApprovalDate);
            updatedBudget.SetDetails(request.Details);
            updatedBudget.SetRegister(DateTime.Now);

            await _repository.SaveChangesAsync();

            return await _appService.GetById(updatedBudget.ID);
        }
    }
}
