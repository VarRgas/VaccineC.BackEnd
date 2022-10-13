using MediatR;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using VaccineC.Command.Application.Commands.BudgetHistoric;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

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
        private readonly IAuthorizationNotificationRepository _authorizationNotificationRepository;
        private readonly VaccineCContext _context;

        public DeleteAuthorizationCommandHandler(IAuthorizationRepository repository, IAuthorizationAppService appService, VaccineCCommandContext ctx, IMediator mediator, IEventRepository eventRepository, IBudgetProductRepository budgetProductRepository, IBudgetRepository budgetRepository, IAuthorizationNotificationRepository authorizationNotificationRepository, VaccineCContext context)
        {
            _repository = repository;
            _appService = appService;
            _ctx = ctx;
            _mediator = mediator;
            _eventRepository = eventRepository;
            _budgetProductRepository = budgetProductRepository;
            _budgetRepository = budgetRepository;
            _authorizationNotificationRepository = authorizationNotificationRepository;
            _context = context; 
        }

        public async Task<IEnumerable<AuthorizationViewModel>> Handle(DeleteAuthorizationCommand request, CancellationToken cancellationToken)
        {
            var authorization = _repository.GetById(request.Id);

            if (authorization == null)
            {
                throw new ArgumentException("Autorização não encontrada!");
            }

            if (authorization.Notify.Equals("S")) {
                await deleteAuthorizatioNotification(authorization.ID);
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

        private async Task<Unit> deleteAuthorizatioNotification(Guid id)
        {

            var idSearch = (from p in _context.AuthorizationsNotifications
                                             where p.AuthorizationId.Equals(id)
                                             select p.ID).FirstOrDefault();

            var authorizationNotification =_authorizationNotificationRepository.GetById(idSearch);

            if (authorizationNotification != null) {

                await deleteSMSNotification(authorizationNotification.ReturnId);
             
            }

            return Unit.Value;
        }

        private async Task<Unit> deleteSMSNotification(string? returnId)
        {
            string url = "https://api.smsdev.com.br/v1/cancel";
            string key = "M30A09QH6Z80WHY0DFS9QECUBIBUVBVT67P50CY9BYSL54W6A504FO9XLB5VLLAD7Y6WUW9PELVVI90LNCYA05RSJU0LY9MIXYIZ06VOQVZXXAJ9N45LQ25QS7IS5V7B";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["key"] = key;
                data["id"] = returnId;
               
                var response = wb.UploadValues(url, "POST", data);
                string responseInString = Encoding.UTF8.GetString(response);

                
            }

            return Unit.Value;
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
