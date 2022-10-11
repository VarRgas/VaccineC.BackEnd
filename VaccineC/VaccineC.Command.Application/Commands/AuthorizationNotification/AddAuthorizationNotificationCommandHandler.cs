using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.AuthorizationNotification
{
    public class AddAuthorizationNotificationCommandHandler : IRequestHandler<AddAuthorizationNotificationCommand, Unit>
    {
        private readonly IAuthorizationNotificationRepository _repository;

        public AddAuthorizationNotificationCommandHandler(IAuthorizationNotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddAuthorizationNotificationCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.AuthorizationNotification newAuthorizationNotification = new Domain.Entities.AuthorizationNotification(
                Guid.NewGuid(), 
                request.AuthorizationId,
                request.EventId,
                request.PersonPhone,
                request.Message,
                request.SendDate,
                request.SendHour,
                DateTime.Now); 

            _repository.Add(newAuthorizationNotification);
            await _repository.SaveChangesAsync(); 

            return Unit.Value;
        }
    }
}
