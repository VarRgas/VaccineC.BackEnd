using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IBudgetAppService
    {
        Task<IEnumerable<BudgetViewModel>> GetAllAsync();
        Task<IEnumerable<BudgetViewModel>> GetByName(string personName);
        Task<IEnumerable<BudgetViewModel>> GetAllByBudgetNumber(int budgetNumber);
        Task<IEnumerable<BudgetViewModel>> GetAllByBorrower(Guid borrowerId);
        Task<IEnumerable<BudgetViewModel>> GetAllByResponsible(Guid responsibleId);
        Task<BudgetDashInfoViewModel> GetBudgetsDashInfo(int month, int year);
        Task<BudgetViewModel> GetById(Guid id);
    }
}
