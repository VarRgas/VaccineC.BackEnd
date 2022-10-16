using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class AuthorizationAppService : IAuthorizationAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public AuthorizationAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorizationViewModel>> GetAllAsync()
        {
            var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
            var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).ToList();
            return authorizationsViewModel;
        }

        public async Task<IEnumerable<AuthorizationViewModel>> GetAllByAuthNumber(int authNumber, string situation, Guid responsibleId)
        {
            if (situation.Equals("T") && responsibleId == Guid.Empty)
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.AuthorizationNumber == authNumber).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }
            else if (!situation.Equals("T") && responsibleId == Guid.Empty)
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.AuthorizationNumber == authNumber && a.Situation.Equals(situation)).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }
            else if (situation.Equals("T") && responsibleId != Guid.Empty)
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.AuthorizationNumber == authNumber && a.BudgetProduct.Budget.PersonId.Equals(responsibleId)).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;

            }
            else
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.AuthorizationNumber == authNumber && a.Situation.Equals(situation) && a.BudgetProduct.Budget.PersonId.Equals(responsibleId)).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }
        }

        public async Task<IEnumerable<AuthorizationViewModel>> GetAllByBorrowerName(string borrowerName, string situation, Guid responsibleId)
        {

            if (borrowerName.Count() < 3)
            {
                throw new ArgumentException("É necessário informar no mínimo 3 caracteres para realizar a busca!");
            }

            if (situation.Equals("T") && responsibleId == Guid.Empty)
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.Person.Name.ToLower().Contains(borrowerName.ToLower())).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }
            else if(!situation.Equals("T") && responsibleId == Guid.Empty)
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.Person.Name.ToLower().Contains(borrowerName.ToLower()) && a.Situation.Equals(situation)).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }
            else if (situation.Equals("T") && responsibleId != Guid.Empty)
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.Person.Name.ToLower().Contains(borrowerName.ToLower()) && a.BudgetProduct.Budget.PersonId.Equals(responsibleId)).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }
            else
            {
                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var authorizationsViewModel = authorizations.Select(r => _mapper.Map<AuthorizationViewModel>(r)).Where(a => a.Person.Name.ToLower().Contains(borrowerName.ToLower()) && a.BudgetProduct.Budget.PersonId.Equals(responsibleId) && a.Situation.Equals(situation)).OrderBy(a => a.Event.StartDate).ToList();

                return authorizationsViewModel;
            }

        }

        public AuthorizationViewModel GetById(Guid id)
        {
            var authorization = _mapper.Map<AuthorizationViewModel>(_queryContext.AllAuthorizations.Where(r => r.ID == id).First());
            return authorization;
        }

        public async Task<IEnumerable<AuthorizationSummarySituationViewModel>> GetSummarySituationAuthorization()
        {

            List<AuthorizationSummarySituationViewModel> listAuthorizationSummarySituationViewModel = new List<AuthorizationSummarySituationViewModel>();

            var products = await _queryContext.AllProducts.ToListAsync();
            var productsViewModel = products.Select(r => _mapper.Map<ProductViewModel>(r)).ToList();

            DateTime now = DateTime.Today;
            DateTime firstDay = new DateTime(now.Year, now.Month, 1);
            DateTime lastDay = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

            foreach (var product in productsViewModel)
            {

                AuthorizationSummarySituationViewModel authorizationSummarySituationViewModel = new AuthorizationSummarySituationViewModel();

                var productsSummariesBatches = await _queryContext.AllProductsSummariesBatches.ToListAsync();
                var totalUnitsProduct = productsSummariesBatches
                    .Select(r => _mapper.Map<ProductSummaryBatchViewModel>(r))
                    .Where(r => r.ProductsId == product.ID)
                    .Sum(r => r.NumberOfUnitsBatch);

                var authorizations = await _queryContext.AllAuthorizations.ToListAsync();
                var totalAuthorizationsByProduct = authorizations
                    .Select(r => _mapper.Map<AuthorizationViewModel>(r))
                        .Where(r => r.BudgetProduct.ProductId == product.ID && r.Event.StartDate >= firstDay && r.Event.StartDate <= lastDay && r.Situation.Equals("C")).Count();

                authorizationSummarySituationViewModel.ProductId = product.ID;
                authorizationSummarySituationViewModel.ProductName = product.Name;
                authorizationSummarySituationViewModel.TotalUnitsProduct = (int)totalUnitsProduct;
                authorizationSummarySituationViewModel.TotalAuthorizationsMonth = totalAuthorizationsByProduct;
                authorizationSummarySituationViewModel.TotalUnitsAfterApplication = (int)totalUnitsProduct - totalAuthorizationsByProduct;

                if (totalAuthorizationsByProduct != 0) {
                    listAuthorizationSummarySituationViewModel.Add(authorizationSummarySituationViewModel);
                }
            }

            return listAuthorizationSummarySituationViewModel;
        }

    }
}
