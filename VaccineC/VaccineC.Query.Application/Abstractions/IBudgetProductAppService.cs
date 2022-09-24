using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IBudgetProductAppService
    {
        Task<IEnumerable<BudgetProductViewModel>> GetAllAsync();
        Task<IEnumerable<BudgetProductViewModel>> GetAllBudgetsProductsByBudgetId(Guid companyId);
        BudgetProductViewModel GetById(Guid id);
    }
}
