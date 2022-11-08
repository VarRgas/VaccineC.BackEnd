using MediatR;
using VaccineC.Command.Application.Commands.BudgetHistoric;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Command.Application.Commands.Budget
{
    public class ManageBudgetOverdueCommandHandler : IRequestHandler<ManageBudgetOverdueCommand, Unit>
    {
        private readonly IBudgetRepository _repository;
        private readonly VaccineCContext _context;

        public ManageBudgetOverdueCommandHandler(IBudgetRepository repository, VaccineCContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<Unit> Handle(ManageBudgetOverdueCommand request, CancellationToken cancellationToken)
        {

            DateTime now = DateTime.Now;

            List<string> budgetSituations = new List<string>();
            budgetSituations.Add("P");
            budgetSituations.Add("E");

            var budgetsOverdueId = (from b in _context.Budgets
                                    where b.ExpirationDate <= now.Date
                                    where budgetSituations.Contains(b.Situation)
                                    select b.ID).ToList();

            if (budgetsOverdueId.Count > 0)
            {
                foreach (var budgetOverdueId in budgetsOverdueId)
                {
                    var budget = _repository.GetById(budgetOverdueId);

                    if (budget != null)
                    {
                        budget.SetSituation("V");
                        budget.SetRegister(DateTime.Now);
                        await _repository.SaveChangesAsync();
                    }
                }
            }

            return Unit.Value;
        }
    }
}
