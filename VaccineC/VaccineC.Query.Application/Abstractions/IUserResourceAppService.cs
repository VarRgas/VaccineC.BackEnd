using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IUserResourceAppService
    {
        Task<IEnumerable<UserResourceViewModel>> GetAllAsync();
        Task<IEnumerable<UserResourceViewModel>> GetAllByUser(Guid userId);
        UserResourceViewModel GetById(Guid id);
    }
}
