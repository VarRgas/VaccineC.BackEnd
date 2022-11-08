using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IUserResourceAppService
    {
        Task<IEnumerable<UserResourceViewModel>> GetAllAsync();
        Task<IEnumerable<UserResourceViewModel>> GetAllByUser(Guid userId);
        Task<UserResourceMenuViewModel> GetUserResourceMenyByUser(Guid userId);
        UserResourceViewModel GetById(Guid id);
        UserResourceViewModel GetByUserResource(Guid usersId, Guid resourcesId);
    }
}
