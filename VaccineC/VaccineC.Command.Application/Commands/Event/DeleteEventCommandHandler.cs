using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Event
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventClass = _eventRepository.GetById(request.Id);

            if (eventClass == null)
            {
                throw new ArgumentException("Evento não encontrado!");
            }

            _eventRepository.Remove(eventClass);
            await _eventRepository.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
