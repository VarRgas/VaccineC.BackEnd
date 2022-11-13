using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IUserAppService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<IEnumerable<UserViewModel>> GetAllActive();
        Task<IEnumerable<UserViewModel>> GetByEmail(String email);
        Task<Boolean> GetUserPermission(Guid id, string currentUrl);
        UserViewModel GetById(Guid id);
    }
}
