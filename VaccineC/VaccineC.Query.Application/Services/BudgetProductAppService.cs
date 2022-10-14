using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class BudgetProductAppService : IBudgetProductAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public BudgetProductAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> GetAllAsync()
        {
            var budgetProducts = await _queryContext.AllBudgetsProducts.ToListAsync();
            var budgetProductsViewModel = budgetProducts.Select(r => _mapper.Map<BudgetProductViewModel>(r)).ToList();
            return budgetProductsViewModel;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> GetAllBudgetsProductsByBudgetId(Guid budgetId)
        {
            var budgetsProducts = await _queryContext.AllBudgetsProducts.Where(bp => bp.BudgetId == budgetId).ToListAsync();
            var budgetsProductsViewModel = budgetsProducts.Select(r => _mapper.Map<BudgetProductViewModel>(r)).ToList();
            return budgetsProductsViewModel;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> GetAllPendingBudgetsProductsByBorrower(Guid budgetId, Guid borrowerId, DateTime startDate)
        {
            var startDateFormated = TimeZoneInfo.ConvertTime(startDate, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            var budgetsProducts = await _queryContext.AllBudgetsProducts.Where(bp => bp.BudgetId == budgetId && bp.BorrowerPersonId == borrowerId && bp.SituationProduct.Equals("P")).ToListAsync();
            var budgetsProductsViewModel = budgetsProducts.Select(r => _mapper.Map<BudgetProductViewModel>(r)).ToList();

            if (budgetsProductsViewModel.Count() > 0)
            {
                budgetsProductsViewModel[0].ApplicationDate = startDateFormated;
            }

            return budgetsProductsViewModel;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> GetAllPendingBudgetsProductsByResponsible(Guid budgetId, DateTime startDate)
        {

            var startDateFormated = TimeZoneInfo.ConvertTime(startDate, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            var budgetsProducts = await _queryContext.AllBudgetsProducts.Where(bp => bp.BudgetId == budgetId && bp.SituationProduct.Equals("P")).ToListAsync();
            var budgetsProductsViewModel = budgetsProducts.Select(r => _mapper.Map<BudgetProductViewModel>(r)).ToList();

            if (budgetsProductsViewModel.Count() > 0)
            {
                budgetsProductsViewModel[0].ApplicationDate = startDateFormated;
            }

            return budgetsProductsViewModel;
        }

        public BudgetProductViewModel GetById(Guid id)
        {
            var budgetProduct = _mapper.Map<BudgetProductViewModel>(_queryContext.AllBudgetsProducts.Where(r => r.ID == id).First());
            return budgetProduct;
        }
    }
}
