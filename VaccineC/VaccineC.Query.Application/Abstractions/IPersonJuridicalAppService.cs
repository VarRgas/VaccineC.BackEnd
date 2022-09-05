using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IPersonJuridicalAppService
    {
        Task<IEnumerable<PersonsJuridicalViewModel>> GetAllAsync();
    }
}
