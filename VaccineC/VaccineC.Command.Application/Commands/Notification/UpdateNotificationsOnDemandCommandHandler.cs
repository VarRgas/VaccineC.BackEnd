using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Notification
{
    public class UpdateNotificationsOnDemandCommandHandler : IRequestHandler<UpdateNotificationsOnDemandCommand, IEnumerable<NotificationViewModel>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationAppService _notificationAppService;
        private readonly VaccineCCommandContext _ctx;

        public UpdateNotificationsOnDemandCommandHandler(INotificationRepository notificationRepository, VaccineCCommandContext ctx, INotificationAppService notificationAppService)
        {
            _notificationRepository = notificationRepository;
            _ctx = ctx;
            _notificationAppService = notificationAppService;
        }

        public async Task<IEnumerable<NotificationViewModel>> Handle(UpdateNotificationsOnDemandCommand request, CancellationToken cancellationToken)
        {
            Guid userId = Guid.NewGuid();

            List<NotificationViewModel> listNotificationViewModel = request.ListNotificationViewModel;

            foreach(var notificationViewModel in listNotificationViewModel)
            {
                var updatedNotification = _notificationRepository.GetById(notificationViewModel.ID);
                updatedNotification.SetSituation("L");
                updatedNotification.SetRegister(DateTime.Now);
                userId = updatedNotification.UserId;

                await _notificationRepository.SaveChangesAsync();
            }

            return await _notificationAppService.GetAllByUser(userId);
        }
    }
}
