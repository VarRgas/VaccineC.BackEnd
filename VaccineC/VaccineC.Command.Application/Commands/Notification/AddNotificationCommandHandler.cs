using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Notification
{
    public class AddNotificationCommandHandler : IRequestHandler<AddNotificationCommand, NotificationViewModel>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly VaccineCCommandContext _ctx;

        public AddNotificationCommandHandler(INotificationRepository notificationRepository, VaccineCCommandContext ctx)
        {
            _notificationRepository = notificationRepository;
            _ctx = ctx;
        }

        public async Task<NotificationViewModel> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Notification newNotification = new Domain.Entities.Notification(
                Guid.NewGuid(),
                request.UserId,
                request.Message,
                request.MessageType,
                request.Situation,
                DateTime.Now);

            _notificationRepository.Add(newNotification);
            await _notificationRepository.SaveChangesAsync();

            return new NotificationViewModel()
            {
                ID = newNotification.ID,
                UserId = newNotification.UserId,
                Message = newNotification.Message,
                MessageType = newNotification.MessageType,
                Situation = newNotification.Situation,
                Register = newNotification.Register
            };
        }
    }
}
