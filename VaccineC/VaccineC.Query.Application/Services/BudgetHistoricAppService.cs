using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class BudgetHistoricAppService : IBudgetHistoricAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public BudgetHistoricAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BudgetHistoricViewModel>> GetAllAsync()
        {
            var budgetsHistorics = await _queryContext.AllBudgetsHistorics.ToListAsync();
            var budgetsHistoricsViewModel = budgetsHistorics.Select(r => _mapper.Map<BudgetHistoricViewModel>(r)).ToList();
            return budgetsHistoricsViewModel;
        }

        public async Task<IEnumerable<BudgetHistoricViewModel>> GetAllBudgetsHistoricsByBudgetId(Guid budgetId)
        {
            var budgetsHistorics = await _queryContext.AllBudgetsHistorics.Where(bp => bp.BudgetId == budgetId).ToListAsync();
            var budgetsHistoricsViewModel = budgetsHistorics.Select(r => _mapper.Map<BudgetHistoricViewModel>(r)).ToList();
            return budgetsHistoricsViewModel;

        }

        public BudgetHistoricViewModel GetById(Guid id)
        {
            var budgetHistoric = _mapper.Map<BudgetHistoricViewModel>(_queryContext.AllBudgetsHistorics.Where(r => r.ID == id).First());
            return budgetHistoric;
        }
    }
}
