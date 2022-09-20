using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Notification
{
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, IEnumerable<NotificationViewModel>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationAppService _notificationAppService;
        private readonly VaccineCCommandContext _ctx;

        public UpdateNotificationCommandHandler(INotificationRepository notificationRepository, VaccineCCommandContext ctx, INotificationAppService notificationAppService)
        {
            _notificationRepository = notificationRepository;
            _ctx = ctx;
            _notificationAppService = notificationAppService;
        }

        public async Task<IEnumerable<NotificationViewModel>> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var updatedNotification = _notificationRepository.GetById(request.ID);
            updatedNotification.SetSituation(request.Situation);

            await _notificationRepository.SaveChangesAsync();

            return await _notificationAppService.GetAllByUser(updatedNotification.UserId);
        }
    }
}
