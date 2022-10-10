using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class UpdateAuthorizationCommandHandler : IRequestHandler<UpdateAuthorizationCommand, IEnumerable<AuthorizationViewModel>>
    {
        private readonly IAuthorizationRepository _repository;
        private readonly IAuthorizationAppService _appService;
        private readonly IEventRepository _eventRepository;
        private readonly ICompanyParameterRepository _companyParameterRepository;

        public UpdateAuthorizationCommandHandler(IAuthorizationRepository repository, IAuthorizationAppService appService, IEventRepository eventRepository, ICompanyParameterRepository companyParameterRepository)
        {
            _repository = repository;
            _appService = appService;
            _eventRepository = eventRepository;
            _companyParameterRepository = companyParameterRepository;   
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
            //var endFormated = TimeZoneInfo.ConvertTime(end, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            var endTimeEvent = end.TimeOfDay;
            var endDateEvent = end.Date;

            eventClass.SetStartDate(request.StartDateEvent);
            eventClass.SetStartTime(request.StartTimeEvent);
            eventClass.SetEndDate(endDateEvent);
            eventClass.SetEndTime(endTimeEvent);
            eventClass.SetRegister(DateTime.Now);

            await _eventRepository.SaveChangesAsync();

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
    }
}
