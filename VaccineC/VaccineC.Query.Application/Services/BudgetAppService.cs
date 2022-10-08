using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Application.Services
{
    public class BudgetAppService : IBudgetAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public BudgetAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context; 
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

        public async Task<IEnumerable<BudgetViewModel>> GetAllByBudgetNumber(int budgetNumber)
        {
            var budgets = await _queryContext.AllBudgets.ToListAsync();
            var budgetsViewModel = budgets
                .Select(r => _mapper.Map<BudgetViewModel>(r))
                .Where(r => r.BudgetNumber == budgetNumber).ToList();
            return budgetsViewModel;
        }

        public async Task<IEnumerable<BudgetViewModel>> GetAllByBorrower(Guid borrowerId)
        {

            var budgetsId = (from b in _context.Budgets
                            join bp in _context.BudgetsProducts on b.ID equals bp.BudgetId
                            where bp.BorrowerPersonId.Equals(borrowerId) && bp.SituationProduct.Equals("P")
                            select b).Select(b => b.ID).Distinct().ToList();

            var budgets = (from b in _context.Budgets
                          where budgetsId.Contains(b.ID) && b.Situation.Equals("A")
                          orderby b.Register descending
                          select b).ToList();

            var budgetsViewModel = _mapper.Map<IEnumerable<BudgetViewModel>>(budgets);

            foreach(var budget in budgetsViewModel)
            {
                var person = (from p in _context.Persons
                                where p.ID.Equals(budget.PersonId)
                                select p).FirstOrDefault();

                var personViewModel = _mapper.Map<PersonViewModel>(person);

                budget.Persons = personViewModel;
            }

            return budgetsViewModel;
        }

        public async Task<BudgetViewModel> GetById(Guid id)
        {
            var budget = _mapper.Map<BudgetViewModel>(_queryContext.AllBudgets.Where(r => r.ID == id).FirstOrDefault());
            return budget;
        }

    }
}
