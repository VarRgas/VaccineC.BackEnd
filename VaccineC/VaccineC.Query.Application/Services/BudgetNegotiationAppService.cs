using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class BudgetNegotiationAppService : IBudgetNegotiationAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public BudgetNegotiationAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BudgetNegotiationViewModel>> GetAllAsync()
        {
            var budgetNegotiations = await _queryContext.AllBudgetsNegotiations.ToListAsync();
            var budgetNegotiationsViewModel = budgetNegotiations.Select(r => _mapper.Map<BudgetNegotiationViewModel>(r)).ToList();
            return budgetNegotiationsViewModel;
        }

        public async Task<IEnumerable<BudgetNegotiationViewModel>> GetAllBudgetsNegotiationsByBudgetId(Guid budgetId)
        {
            var budgetNegotiations = await _queryContext.AllBudgetsNegotiations.Where(bp => bp.BudgetId == budgetId).ToListAsync();
            var budgetNegotiationsViewModel = budgetNegotiations.Select(r => _mapper.Map<BudgetNegotiationViewModel>(r)).ToList();
            return budgetNegotiationsViewModel;
        }

        public BudgetNegotiationViewModel GetById(Guid id)
        {
            var budgetNegotiation = _mapper.Map<BudgetNegotiationViewModel>(_queryContext.AllBudgetsNegotiations.Where(r => r.ID == id).First());
            return budgetNegotiation;
        }
    }
}
