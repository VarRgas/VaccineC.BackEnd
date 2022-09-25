using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Abstractions
{
    public interface IBudgetNegotiationAppService
    {
        Task<IEnumerable<BudgetNegotiationViewModel>> GetAllAsync();
        Task<IEnumerable<BudgetNegotiationViewModel>> GetAllBudgetsNegotiationsByBudgetId(Guid budgetId);
        BudgetNegotiationViewModel GetById(Guid id);
    }
}
