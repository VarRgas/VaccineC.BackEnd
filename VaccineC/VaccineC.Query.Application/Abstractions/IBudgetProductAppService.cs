using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Abstractions
{
    public interface IBudgetProductAppService
    {
        Task<IEnumerable<BudgetProductViewModel>> GetAllAsync();
        BudgetProductViewModel GetById(Guid id);
    }
}
