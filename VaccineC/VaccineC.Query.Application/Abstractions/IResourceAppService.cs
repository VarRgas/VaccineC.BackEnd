using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IResourceAppService
    {
        Task<IEnumerable<ResourceViewModel>> GetAllAsync();
        ResourceViewModel GetById(Guid id);
    }
}
