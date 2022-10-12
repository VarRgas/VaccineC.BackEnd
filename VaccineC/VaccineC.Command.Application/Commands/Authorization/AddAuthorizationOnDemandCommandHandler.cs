using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.AuthorizationNotification;
using VaccineC.Command.Application.Commands.BudgetHistoric;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class AddAuthorizationOnDemandCommandHandler : IRequestHandler<AddAuthorizationOnDemandCommand, IEnumerable<EventViewModel>>
    {
        private readonly IAuthorizationRepository _repository;
        private readonly IAuthorizationAppService _appService;
        private readonly IEventRepository _eventRepository;
        private readonly IEventAppService _eventAppService;
        private readonly ICompanyParameterRepository _companyParameterRepository;
        private readonly IBudgetProductRepository _budgetProductRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly VaccineCCommandContext _ctx;
        private readonly VaccineCContext _context;
        private readonly IMediator _mediator;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;


        public AddAuthorizationOnDemandCommandHandler(IAuthorizationRepository repository, IAuthorizationAppService appService, IEventRepository eventRepository, IEventAppService eventAppService, ICompanyParameterRepository companyParameterRepository, IBudgetProductRepository budgetProductRepository, VaccineCCommandContext ctx, IMediator mediator, IBudgetRepository budgetRepository, VaccineCContext context, IQueryContext queryContext, IMapper mapper)
        {
            _repository = repository;
            _appService = appService;
            _eventRepository = eventRepository;
            _eventAppService = eventAppService;
            _companyParameterRepository = companyParameterRepository;
            _budgetProductRepository = budgetProductRepository;
            _ctx = ctx;
            _mediator = mediator;
            _context = context;
            _budgetRepository = budgetRepository;
            _mapper = mapper;
            _queryContext = queryContext;
        }

        public async Task<IEnumerable<EventViewModel>> Handle(AddAuthorizationOnDemandCommand request, CancellationToken cancellationToken)
        {
            List<AuthorizationViewModel> listAuthorizationViewModel = request.ListAuthorizationViewModel;

            foreach(AuthorizationViewModel authorizationViewModel in listAuthorizationViewModel)
            {

                var eventSearch = authorizationViewModel.Event;

                await validateEventStartDate(eventSearch);

                var end = (eventSearch.StartDate + eventSearch.StartTime).AddMinutes(await getApplicationTimePerMinute());
                var endFormated = TimeZoneInfo.ConvertTime(end, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

                var endTime = endFormated.TimeOfDay;
                var endDate = endFormated.Date;

                Domain.Entities.Event newEvent = new Domain.Entities.Event(
                   Guid.NewGuid(),
                   eventSearch.UserId,
                   eventSearch.Situation,
                   eventSearch.Concluded,
                   eventSearch.StartDate,
                   endDate,
                   eventSearch.StartTime,
                   endTime,
                   eventSearch.Details,
                   DateTime.Now
                   );

                _eventRepository.Add(newEvent);
                await _eventRepository.SaveChangesAsync();

                Domain.Entities.Authorization newAuthorization = new Domain.Entities.Authorization(
                Guid.NewGuid(),
                authorizationViewModel.UserId,
                newEvent.ID,
                authorizationViewModel.BudgetProductId,
                authorizationViewModel.BorrowerPersonId,
                authorizationViewModel.AuthorizationNumber,
                authorizationViewModel.Situation,
                authorizationViewModel.TypeOfService,
                authorizationViewModel.Notify,
                authorizationViewModel.AuthorizationDate,
                DateTime.Now
                );

                _repository.Add(newAuthorization);
                await _repository.SaveChangesAsync();

                if (newAuthorization.Notify.Equals("S"))
                {
                    await createAuthorizationNotification(newAuthorization, newEvent, authorizationViewModel.PersonPhone);
                }

                var budgetProduct = _budgetProductRepository.GetById(authorizationViewModel.BudgetProductId);

                if (budgetProduct == null)
                {
                    throw new ArgumentException("Orçamento Produto não encontrado!");
                }

                budgetProduct.SetSituationProduct("E");
                await _budgetProductRepository.SaveChangesAsync();

                await treatBudget(budgetProduct.BudgetId, authorizationViewModel.UserId);
            }

            return await _eventAppService.GetAllAsync();
        }

        private async Task<Unit> validateEventStartDate(Query.Model.Models.Event? eventSearch)
        {
            DateTime date = eventSearch.StartDate;
            DateTime before = new DateTime(2010, 01, 01);
            DateTime after = new DateTime(2055, 01, 01);

            if (date <= before)
            {
                throw new ArgumentException("Data(s) informada(s) inválida(s), verifique!");
            }

            if (date >= after)
            {
                throw new ArgumentException("Data(s) informada(s) inválida(s), verifique!");
            }

            return Unit.Value;
        }

        private async Task<Unit> createAuthorizationNotification(Domain.Entities.Authorization authorization, Domain.Entities.Event eventClass, string personPhone)
        {
            var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
            var authorizationViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.ID == authorization.ID).FirstOrDefault();

            string dateMessage = eventClass.StartDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string message = "";

            if (authorizationViewModel.BudgetProduct.ProductDose.Equals(null) || authorizationViewModel.BudgetProduct.ProductDose.Equals(""))
            {
                message = $"LEMBRETE: {authorizationViewModel.Person.Name.Split(" ")[0]}, a aplicação de {authorizationViewModel.BudgetProduct.Product.Name} está agendada para {dateMessage} {eventClass.StartTime.ToString(@"hh\:mm")}";
            }
            else
            {
                message = $"LEMBRETE: {authorizationViewModel.Person.Name.Split(" ")[0]}, a aplicação de {authorizationViewModel.BudgetProduct.Product.Name} ({doseFormated(authorizationViewModel.BudgetProduct.ProductDose)}) está agendada para {dateMessage} {eventClass.StartTime.ToString(@"hh\:mm")}";

            }

            await _mediator.Send(new AddAuthorizationNotificationCommand(
                 Guid.NewGuid(),
                 authorization.ID,
                 authorization.EventId,
                 personPhone,
                 message,
                 eventClass.StartDate.Date.AddDays(-1),
                 eventClass.StartTime,
                 DateTime.Now,
                 null
                 ));


            return Unit.Value;
        }

        public async Task<double> getApplicationTimePerMinute()
        {
            var companyParameter = _companyParameterRepository.GetAll().FirstOrDefault();

            if(companyParameter == null)
            {
                throw new ArgumentException("Parâmetros não encontrados!");
            }

            return (double) companyParameter.ApplicationTimePerMinute;
        }

        public async Task<Unit> treatBudget(Guid budgetId, Guid userId) {

            var budgetsProducts = (from bp in _context.BudgetsProducts
                                   where bp.BudgetId.Equals(budgetId)
                                   select bp).ToList();

            if (budgetsProducts.Count == 0)
            {
                throw new ArgumentException("Orçamento Produto não encontrado!");
            }

            int aux = 0;

            foreach(var budgetProduct in budgetsProducts)
            {
                if (budgetProduct.SituationProduct.Equals("P"))
                {
                    aux++;
                }
            }

            if(aux == 0)
            {
                var budget = _budgetRepository.GetById(budgetId);

                if(budget == null)
                {
                  throw new ArgumentException("Orçamento não encontrado!");
                }

                string formerSituation = budget.Situation;

                budget.SetSituation("F");
                budget.SetRegister(DateTime.Now);
                await _budgetRepository.SaveChangesAsync();

                await _mediator.Send(new AddBudgetHistoricCommand(
                    Guid.NewGuid(),
                    budget.ID,
                    userId,
                    "Situação do Orçamento alterada: de " + situationFormated(formerSituation) + " para " + situationFormated(budget.Situation) + ".",
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

        public string doseFormated(string doseType)
        {
            if (doseType.Equals("DU")) 
            {
                return "DOSE ÚNICA";
            }else if (doseType.Equals("D1"))
            {
                return "DOSE 1";
            }else if (doseType.Equals("D2"))
            {
                return "DOSE 2";
            }else if (doseType.Equals("D3"))
            {
                return "DOSE 3";
            }else if (doseType.Equals("DR"))
            {
                return "DOSE DE REFORÇO";
            }else
            {
                return "";
            }
        }
    }
}
