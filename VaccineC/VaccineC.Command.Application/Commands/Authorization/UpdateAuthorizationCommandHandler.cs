using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using VaccineC.Command.Application.Commands.AuthorizationNotification;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class UpdateAuthorizationCommandHandler : IRequestHandler<UpdateAuthorizationCommand, IEnumerable<AuthorizationViewModel>>
    {
        private readonly IAuthorizationRepository _repository;
        private readonly IAuthorizationAppService _appService;
        private readonly IEventRepository _eventRepository;
        private readonly ICompanyParameterRepository _companyParameterRepository;
        private readonly IAuthorizationNotificationRepository _authorizationNotificationRepository;
        private readonly VaccineCContext _context;
        private readonly IMediator _mediator;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public UpdateAuthorizationCommandHandler(IAuthorizationRepository repository, IAuthorizationAppService appService, IEventRepository eventRepository, ICompanyParameterRepository companyParameterRepository, IAuthorizationNotificationRepository authorizationNotificationRepository, VaccineCContext context, IMediator mediator, IQueryContext queryContext, IMapper mapper)
        {
            _repository = repository;
            _appService = appService;
            _eventRepository = eventRepository;
            _companyParameterRepository = companyParameterRepository;
            _authorizationNotificationRepository = authorizationNotificationRepository;
            _context = context;
            _mediator = mediator;
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorizationViewModel>> Handle(UpdateAuthorizationCommand request, CancellationToken cancellationToken)
        {
            var authorization = _repository.GetById(request.ID);

            if (authorization == null)
            {
                throw new ArgumentException("Autorização não encontrada!");
            }

            authorization.SetUserId(request.UserId);
            authorization.SetRegister(DateTime.Now);

            await _repository.SaveChangesAsync();

            var eventClass = _eventRepository.GetById(request.EventId);

            if (eventClass == null)
            {
                throw new ArgumentException("Evento não encontrado!");
            }

            var end = (request.StartDateEvent.Date + request.StartTimeEvent).AddMinutes(await getApplicationTimePerMinute());

            var endTimeEvent = end.TimeOfDay;
            var endDateEvent = end.Date;

            eventClass.SetStartDate(request.StartDateEvent);
            eventClass.SetStartTime(request.StartTimeEvent);
            eventClass.SetEndDate(endDateEvent);
            eventClass.SetEndTime(endTimeEvent);
            eventClass.SetRegister(DateTime.Now);

            await _eventRepository.SaveChangesAsync();


            if (authorization.Notify.Equals("S"))
            {
                await deleteAuthorizatioNotification(authorization.ID);
                await createAuthorizationNotification(authorization, eventClass);
            }


            return await _appService.GetAllAsync();
        }

        public async Task<double> getApplicationTimePerMinute()
        {
            var companyParameter = _companyParameterRepository.GetAll().FirstOrDefault();

            if (companyParameter == null)
            {
                throw new ArgumentException("Parâmetros não encontrados!");
            }

            return (double)companyParameter.ApplicationTimePerMinute;
        }

        private async Task<Unit> deleteAuthorizatioNotification(Guid id)
        {

            var idSearch = (from p in _context.AuthorizationsNotifications
                            where p.AuthorizationId.Equals(id)
                            select p.ID).FirstOrDefault();

            var authorizationNotification = _authorizationNotificationRepository.GetById(idSearch);

            if (authorizationNotification != null)
            {

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

        private async Task<Unit> createAuthorizationNotification(Domain.Entities.Authorization authorization, Domain.Entities.Event eventClass)
        {

            var idSearch = (from p in _context.AuthorizationsNotifications
                            where p.AuthorizationId.Equals(authorization.ID)
                            select p.ID).FirstOrDefault();

            var authorizationNotification = _authorizationNotificationRepository.GetById(idSearch);
           
            if (authorizationNotification != null)
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
                     authorizationNotification.PersonPhone,
                     message,
                     eventClass.StartDate.Date.AddDays(-1),
                     eventClass.StartTime,
                     DateTime.Now,
                     null
                     ));
            }

            var toRemove = _authorizationNotificationRepository.GetById(authorizationNotification.ID);
            
            if (toRemove != null) {
                _authorizationNotificationRepository.Remove(toRemove);
               await _authorizationNotificationRepository.SaveChangesAsync();
            }

            return Unit.Value;
        }

        public string doseFormated(string doseType)
        {
            if (doseType.Equals("DU"))
            {
                return "DOSE ÚNICA";
            }
            else if (doseType.Equals("D1"))
            {
                return "DOSE 1";
            }
            else if (doseType.Equals("D2"))
            {
                return "DOSE 2";
            }
            else if (doseType.Equals("D3"))
            {
                return "DOSE 3";
            }
            else if (doseType.Equals("DR"))
            {
                return "DOSE DE REFORÇO";
            }
            else
            {
                return "";
            }
        }
    }
}
