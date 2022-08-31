using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Abstractions
{
    public interface ICompanyScheduleAppService
    {
        Task<IEnumerable<CompanyScheduleViewModel>> GetAllAsync();
        CompanyScheduleViewModel GetById(Guid id);
    }
}
