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

        public BudgetProductViewModel GetById(Guid id)
        {
            var budgetProduct = _mapper.Map<BudgetProductViewModel>(_queryContext.AllBudgetsProducts.Where(r => r.ID == id).First());
            return budgetProduct;
        }
    }
}
