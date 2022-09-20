using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Notification
{
    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, IEnumerable<NotificationViewModel>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationAppService _notificationAppService;

        public DeleteNotificationCommandHandler(INotificationRepository notificationRepository, INotificationAppService notificationAppService)
        {
            _notificationRepository = notificationRepository;
            _notificationAppService = notificationAppService;
        }

        public async Task<IEnumerable<NotificationViewModel>> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = _notificationRepository.GetById(request.Id);
            _notificationRepository.Remove(notification);
            await _notificationRepository.SaveChangesAsync();

            return await _notificationAppService.GetAllByUser(notification.UserId);
        }
    }
}
