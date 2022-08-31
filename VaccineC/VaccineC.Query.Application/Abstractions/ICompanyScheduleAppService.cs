using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Abstractions
{
    public interface ICompanyScheduleAppService
    {
        Task<IEnumerable<CompanyScheduleViewModel>> GetAllAsync();
        Task<IEnumerable<CompanyScheduleViewModel>> GetAllCompaniesSchedulesByCompanyID(Guid companyId);
        CompanyScheduleViewModel GetById(Guid id);
    }
}
