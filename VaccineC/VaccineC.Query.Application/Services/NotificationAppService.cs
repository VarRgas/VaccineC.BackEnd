using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
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

            DateTime today = DateTime.Now;
            TimeSpan minHour = new TimeSpan(0, 0, 0);
            TimeSpan maxHour = new TimeSpan(23, 59, 0);
            DateTime todayMin = today.Date + minHour;
            DateTime todayMax = today.Date + maxHour;

            var notifications = await _queryContext.AllNotifications.ToListAsync();
            var notificationsViewModel = notifications
                .Select(r => _mapper.Map<NotificationViewModel>(r))
                .Where(r => r.UserId == userID && r.Register >= todayMin && r.Register <= todayMax)
                .ToList();

            foreach(var notification in notificationsViewModel)
            {
                notification.FormatedDate = getFormatedDate(notification.Register);
            }

            return notificationsViewModel;
        }

        public NotificationViewModel GetById(Guid id)
        {
            var notification = _mapper.Map<NotificationViewModel>(_queryContext.AllNotifications.Where(r => r.ID == id).First());
            return notification;
        }

        private string? getFormatedDate(DateTime register)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            int dia = register.Day;
            int ano = register.Year;
            string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(register.Month));
            string data = dia + " de " + mes + ", " + ano;

            return data;
        }
    }
}
