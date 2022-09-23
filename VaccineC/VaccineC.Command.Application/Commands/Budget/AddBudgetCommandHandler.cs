using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Budget
{
    public class AddBudgetCommandHandler : IRequestHandler<AddBudgetCommand, BudgetViewModel>
    {
        private readonly IBudgetRepository _repository;
        private readonly VaccineCCommandContext _ctx;

        public AddBudgetCommandHandler(IBudgetRepository repository, VaccineCCommandContext ctx)
        {
            _repository = repository;
            _ctx = ctx;
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

            return new BudgetViewModel()
            {
                ID = newBudget.ID,
                UserId = newBudget.UserId,
                PersonId = newBudget.PersonId,
                DiscountPercentage = newBudget.DiscountPercentage,
                DiscountValue = newBudget.DiscountValue,
                TotalBudgetAmount = newBudget.TotalBudgetAmount,
                TotalBudgetedAmount = newBudget.TotalBudgetedAmount,
                ExpirationDate = newBudget.ExpirationDate,
                ApprovalDate = newBudget.ApprovalDate,
                Details = newBudget.Details,
                BudgetNumber = newBudget.BudgetNumber,
                Register = newBudget.Register,
            };
        }
    }
}
