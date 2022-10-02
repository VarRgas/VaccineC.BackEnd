using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IBudgetHistoricAppService
    {
        Task<IEnumerable<BudgetHistoricViewModel>> GetAllAsync();
        Task<IEnumerable<BudgetHistoricViewModel>> GetAllBudgetsHistoricsByBudgetId(Guid budgetId);
        BudgetHistoricViewModel GetById(Guid id);
    }
}
