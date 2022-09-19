using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class NotificationAppService : INotificationAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public NotificationAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationViewModel>> GetAllAsync()
        {
            var notifications = await _queryContext.AllNotifications.ToListAsync();
            var notificationsViewModel = notifications.Select(r => _mapper.Map<NotificationViewModel>(r)).ToList();
            return notificationsViewModel;
        }

        public async Task<IEnumerable<NotificationViewModel>> GetAllByUser(Guid userID)
        {
            var notifications = await _queryContext.AllNotifications.ToListAsync();
            var notificationsViewModel = notifications
                .Select(r => _mapper.Map<NotificationViewModel>(r))
                .Where(r => r.UserId == userID)
                .ToList();
            return notificationsViewModel;
        }

        public NotificationViewModel GetById(Guid id)
        {
            var notification = _mapper.Map<NotificationViewModel>(_queryContext.AllNotifications.Where(r => r.ID == id).First());
            return notification;
        }
    }
}
