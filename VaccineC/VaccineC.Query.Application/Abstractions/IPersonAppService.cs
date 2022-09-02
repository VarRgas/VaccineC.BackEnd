using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IPersonAppService
    {
        Task<IEnumerable<PersonViewModel>> GetAllAsync();
        Task<IEnumerable<PersonViewModel>> GetAllUserAutocomplete();
        Task<IEnumerable<PersonViewModel>> GetAllCompanyAutocomplete();
        Task<IEnumerable<PersonViewModel>> GetAllByType(string personType);
        Task<IEnumerable<PersonViewModel>> GetByName(String name);

    }
}
