using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

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
        private readonly VaccineCCommandContext _ctx;
        private readonly IMediator _mediator;

        public AddAuthorizationOnDemandCommandHandler(IAuthorizationRepository repository, IAuthorizationAppService appService, IEventRepository eventRepository, IEventAppService eventAppService, ICompanyParameterRepository companyParameterRepository, IBudgetProductRepository budgetProductRepository, VaccineCCommandContext ctx, IMediator mediator)
        {
            _repository = repository;
            _appService = appService;
            _eventRepository = eventRepository;
            _eventAppService = eventAppService;
            _companyParameterRepository = companyParameterRepository;
            _budgetProductRepository = budgetProductRepository;
            _ctx = ctx;
            _mediator = mediator;
        }

        public async Task<IEnumerable<EventViewModel>> Handle(AddAuthorizationOnDemandCommand request, CancellationToken cancellationToken)
        {
            List<AuthorizationViewModel> listAuthorizationViewModel = request.ListAuthorizationViewModel;

            foreach(AuthorizationViewModel authorizationViewModel in listAuthorizationViewModel)
            {

                var eventSearch = authorizationViewModel.Event;
                
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

                var budgetProduct = _budgetProductRepository.GetById(authorizationViewModel.BudgetProductId);

                if (budgetProduct == null)
                {
                    throw new ArgumentException("Orçamento Produto não encontrado!");
                }

                budgetProduct.SetSituationProduct("E");
                await _budgetProductRepository.SaveChangesAsync();
            }

            return await _eventAppService.GetAllAsync();
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
    }
}
