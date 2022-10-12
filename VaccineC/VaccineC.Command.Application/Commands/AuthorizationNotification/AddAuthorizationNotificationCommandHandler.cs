using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.AuthorizationNotification
{
    public class AddAuthorizationNotificationCommandHandler : IRequestHandler<AddAuthorizationNotificationCommand, Unit>
    {
        private readonly IAuthorizationNotificationRepository _repository;
        private readonly IMediator _mediator;

        public AddAuthorizationNotificationCommandHandler(IAuthorizationNotificationRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;   
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
                DateTime.Now,
                request.ReturnId); 

            _repository.Add(newAuthorizationNotification);
            await _repository.SaveChangesAsync();

            string jobDate = newAuthorizationNotification.SendDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string jodTime = newAuthorizationNotification.SendHour.ToString(@"hh\:mm");

            await _mediator.Send(new SendMessageSMSCommand(
                newAuthorizationNotification.ID,
                newAuthorizationNotification.PersonPhone,
                newAuthorizationNotification.Message,
                jobDate,
                jodTime
                ));

            return Unit.Value;
        }
    }
}
