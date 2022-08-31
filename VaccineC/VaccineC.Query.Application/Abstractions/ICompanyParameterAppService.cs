using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface ICompanyParameterAppService
    {
        Task<IEnumerable<CompaniesParametersViewModel>> GetAllAsync();
        CompaniesParametersViewModel GetById(Guid id);
    }
}
