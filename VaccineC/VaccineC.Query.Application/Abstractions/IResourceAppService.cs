using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IResourceAppService
    {
        Task<IEnumerable<ResourceViewModel>> GetAllAsync();
        Task<IEnumerable<ResourceViewModel>> GetByName(String name);
        ResourceViewModel GetById(Guid id);
        Task<IEnumerable<ResourceViewModel>> GetByUserResource(Guid userId);
    }
}
