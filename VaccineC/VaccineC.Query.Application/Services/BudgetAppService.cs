using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class BudgetAppService : IBudgetAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public BudgetAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BudgetViewModel>> GetAllAsync()
        {
            var budgets = await _queryContext.AllBudgets.ToListAsync();
            var budgetsViewModel = budgets.Select(r => _mapper.Map<BudgetViewModel>(r)).ToList();
            return budgetsViewModel;
        }

        public async Task<IEnumerable<BudgetViewModel>> GetByName(string personName)
        {

            var budgets = await _queryContext.AllBudgets.ToListAsync();
            var budgetsViewModel = budgets
                .Select(r => _mapper.Map<BudgetViewModel>(r))
                .Where(r => r.Persons.Name.Contains(personName, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return budgetsViewModel;

        }

        public async Task<BudgetViewModel> GetById(Guid id)
        {
            var budget = _mapper.Map<BudgetViewModel>(_queryContext.AllBudgets.Where(r => r.ID == id).FirstOrDefault());
            return budget;
        }
    }
}
