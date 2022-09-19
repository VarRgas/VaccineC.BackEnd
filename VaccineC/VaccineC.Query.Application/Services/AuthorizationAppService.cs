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
                        .Where(r => r.BudgetProduct.ProductId == product.ID && r.AuthorizationDate >= now && r.AuthorizationDate <= lastDay).Count();

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
