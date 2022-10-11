using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Abstractions
{
    public interface IAuthorizationNotificationAppService
    {
        Task<IEnumerable<AuthorizationNotificationViewModel>> GetAllAsync();
        AuthorizationNotificationViewModel GetById(Guid id);
    }
}
