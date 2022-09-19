using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface INotificationAppService
    {
        Task<IEnumerable<NotificationViewModel>> GetAllAsync();
        Task<IEnumerable<NotificationViewModel>> GetAllByUser(Guid userID);
        NotificationViewModel GetById(Guid id);

    }
}
