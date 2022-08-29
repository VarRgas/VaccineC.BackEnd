using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface ICompanyAppService
    {
        Task<IEnumerable<CompanyViewModel>> GetAllAsync();
        Task<IEnumerable<CompanyViewModel>> GetByName(String name);

        CompanyViewModel GetById(Guid id);
    }
}
