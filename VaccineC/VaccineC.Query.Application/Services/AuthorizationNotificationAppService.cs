using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class AuthorizationNotificationAppService : IAuthorizationNotificationAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public AuthorizationNotificationAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorizationNotificationViewModel>> GetAllAsync()
        {
            var authorizationsNotifications = await _queryContext.AllAuthorizationsNotifications.ToListAsync();
            var authorizationsNotificationsViewModel = authorizationsNotifications.Select(r => _mapper.Map<AuthorizationNotificationViewModel>(r)).ToList();
            return authorizationsNotificationsViewModel;
        }

        public AuthorizationNotificationViewModel GetById(Guid id)
        {
            var authorizationNotification = _mapper.Map<AuthorizationNotificationViewModel>(_queryContext.AllAuthorizationsNotifications.Where(r => r.ID == id).First());
            return authorizationNotification;
        }
    }
}
