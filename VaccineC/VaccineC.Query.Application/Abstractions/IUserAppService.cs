using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IUserAppService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<IEnumerable<UserViewModel>> GetAllActive();
        Task<IEnumerable<UserViewModel>> GetByEmail(String email);
        UserViewModel GetById(Guid id);
    }
}
