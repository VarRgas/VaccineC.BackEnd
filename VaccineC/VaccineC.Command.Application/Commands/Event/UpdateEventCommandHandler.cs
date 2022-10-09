using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Event
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Unit>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {

            var eventClass = _eventRepository.GetById(request.ID);

            if(eventClass == null)
            {
                throw new ArgumentException("Evento não encontrado!");
            }

            eventClass.SetSituation(request.Situation);
            eventClass.SetConcluded(request.Concluded);
            eventClass.SetStartDate(request.StartDate);
            eventClass.SetEndDate(request.EndDate);
            eventClass.SetStartTime(request.StartTime);
            eventClass.SetEndTime(request.EndTime);
            eventClass.SetDetails(request.Details);
            eventClass.SetRegister(DateTime.Now);

            await _eventRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
