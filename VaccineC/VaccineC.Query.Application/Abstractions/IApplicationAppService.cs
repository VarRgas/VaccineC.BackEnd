using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IApplicationAppService
    {
        Task<IEnumerable<ApplicationViewModel>> GetAllAsync();
        Task<IEnumerable<ApplicationViewModel>> GetByName(String name);
        ApplicationViewModel GetById(Guid id);
    }
}
