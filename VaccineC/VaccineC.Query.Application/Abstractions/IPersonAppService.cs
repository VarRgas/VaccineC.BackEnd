using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IPersonAppService
    {
        Task<IEnumerable<PersonViewModel>> GetAllAsync();
        Task<IEnumerable<PersonViewModel>> GetAllByType(string personType);
    }
}
