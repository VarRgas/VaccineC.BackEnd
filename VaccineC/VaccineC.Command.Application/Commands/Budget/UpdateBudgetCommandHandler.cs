using MediatR;
using VaccineC.Command.Application.Commands.BudgetHistoric;
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
        private readonly IMediator _mediator;

        public UpdateBudgetCommandHandler(IBudgetRepository repository, VaccineCCommandContext ctx, IBudgetAppService appService, IMediator mediator)
        {
            _repository = repository;
            _ctx = ctx;
            _appService = appService;
            _mediator = mediator;
        }

        public async Task<BudgetViewModel> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
        {
            var updatedBudget = _repository.GetById(request.ID);

            string historic = "";
            string budgetSituation = updatedBudget.Situation;
           
            updatedBudget.SetSituation(request.Situation);
            updatedBudget.SetUserId(request.UserID);
            updatedBudget.SetTotalBudgetAmount(request.TotalBudgetAmount);
            updatedBudget.SetTotalBudgetedAmount(request.TotalBudgetedAmount);
            updatedBudget.SetDiscountPercentage(request.DiscountPercentage);
            updatedBudget.SetDiscountValue(request.DiscountValue);
            updatedBudget.SetExpirationDate(request.ExpirationDate);
            updatedBudget.SetApprovalDate(request.ApprovalDate);
            updatedBudget.SetDetails(request.Details);
            updatedBudget.SetRegister(DateTime.Now);

            await _repository.SaveChangesAsync();

            if (budgetSituation != request.Situation)
            {
                historic = "Situação do Orçamento alterada: de " + situationFormated(budgetSituation) + " para " + situationFormated(request.Situation) + ".";

                await _mediator.Send(new AddBudgetHistoricCommand(
                      Guid.NewGuid(),
                      updatedBudget.ID,
                      updatedBudget.UserId,
                      historic,
                      DateTime.Now
                      ));
            }

            return await _appService.GetById(updatedBudget.ID);
        }


        public string situationFormated(string situation) {

            if (situation.Equals("A")) {
                return "Aprovado";
            }
            else if (situation.Equals("P"))
            {
                return "Pendente";
            }
            else if (situation.Equals("X"))
            {
                return "Cancelado";
            }
            else if (situation.Equals("V"))
            {
                return "Vencido";
            }
            else if (situation.Equals("F"))
            {
                return "Finalizado";
            }
            else if (situation.Equals("E"))
            {
                return "Em Negociação";
            }
            else
            {
                return "";
            }
        }
    }
}
