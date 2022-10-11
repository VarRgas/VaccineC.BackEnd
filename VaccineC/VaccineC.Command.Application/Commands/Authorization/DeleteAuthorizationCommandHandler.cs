using MediatR;
using VaccineC.Command.Application.Commands.BudgetHistoric;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class DeleteAuthorizationCommandHandler : IRequestHandler<DeleteAuthorizationCommand, IEnumerable<AuthorizationViewModel>>
    {
        private readonly IAuthorizationRepository _repository;
        private readonly IAuthorizationAppService _appService;
        private readonly VaccineCCommandContext _ctx;
        private readonly IMediator _mediator;
        private readonly IEventRepository _eventRepository;
        private readonly IBudgetProductRepository _budgetProductRepository;
        private readonly IBudgetRepository _budgetRepository;

        public DeleteAuthorizationCommandHandler(IAuthorizationRepository repository, IAuthorizationAppService appService, VaccineCCommandContext ctx, IMediator mediator, IEventRepository eventRepository, IBudgetProductRepository budgetProductRepository, IBudgetRepository budgetRepository)
        {
            _repository = repository;
            _appService = appService;
            _ctx = ctx;
            _mediator = mediator;
            _eventRepository = eventRepository;
            _budgetProductRepository = budgetProductRepository;
            _budgetRepository = budgetRepository;   
        }

        public async Task<IEnumerable<AuthorizationViewModel>> Handle(DeleteAuthorizationCommand request, CancellationToken cancellationToken)
        {
            var authorization = _repository.GetById(request.Id);

            if (authorization == null)
            {
                throw new ArgumentException("Autorização não encontrada!");
            }

            authorization.SetSituation("X");
            await _repository.SaveChangesAsync();

            var eventClass = _eventRepository.GetById(authorization.EventId);

            if (eventClass == null)
            {
                throw new ArgumentException("Evento não encontrado!");
            }

            eventClass.SetSituation("X");
            await _eventRepository.SaveChangesAsync();

            var budgetProduct = _budgetProductRepository.GetById(authorization.BudgetProductId);

            if (budgetProduct == null)
            {
                throw new ArgumentException("Orçamento Produto não encontrado!");
            }

            budgetProduct.SetSituationProduct("P");
            await _budgetProductRepository.SaveChangesAsync();

            await treatBudget(budgetProduct.BudgetId, request.userId, authorization.AuthorizationNumber);

            return await _appService.GetAllAsync();

        }

        public async Task<Unit> treatBudget(Guid budgetId, Guid userId, int authorizationNumber)
        {
            var budget = _budgetRepository.GetById(budgetId);

            if (budget == null)
            {
                throw new ArgumentException("Orçamento não encontrado!");
            }

            if (budget.Situation.Equals("F"))
            {

                string formerSituation = budget.Situation;

                budget.SetSituation("A");
                budget.SetRegister(DateTime.Now);
                await _budgetRepository.SaveChangesAsync();

                await _mediator.Send(new AddBudgetHistoricCommand(
                    Guid.NewGuid(),
                    budget.ID,
                    userId,
                    "Situação alterada: de " + situationFormated(formerSituation) + " para " + situationFormated(budget.Situation) + ". (Autorização nº " + authorizationNumber + " cancelada)",
                    DateTime.Now
                    ));
            }

            return Unit.Value;
        }

        public string situationFormated(string situation)
        {

            if (situation.Equals("A"))
            {
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
