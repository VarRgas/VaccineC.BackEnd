using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Event
{
    public class AddEventCommandHandler : IRequestHandler<AddEventCommand, Unit>
    {
        private readonly IEventRepository _repository;

        public AddEventCommandHandler(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.Event newEvent = new Domain.Entities.Event(
                Guid.NewGuid(),
                request.UserId,
                request.Situation,
                request.EventType,
                request.Concluded,
                request.StartDate,
                request.EndDate,
                request.StartTime,
                request.EndTime,
                request.Details,
                DateTime.Now
                );

            _repository.Add(newEvent);
            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
