using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class ProductSummaryBatchAppService : IProductSummaryBatchAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public ProductSummaryBatchAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductSummaryBatchViewModel>> GetAllAsync()
        {
            var productsSummariesBatches = await _queryContext.AllProductsSummariesBatches.ToListAsync();
            var productsSummariesBatchesViewModel = productsSummariesBatches.Select(r => _mapper.Map<ProductSummaryBatchViewModel>(r)).ToList();
            return productsSummariesBatchesViewModel;
        }

        public ProductSummaryBatchViewModel GetById(Guid id)
        {
            var productSummaryBatch = _mapper.Map<ProductSummaryBatchViewModel>(_queryContext.AllProductsSummariesBatches.Where(r => r.ID == id).First());
            return productSummaryBatch;
        }

        public async Task<ProductSummaryBatchViewModel> GetByName(Guid id, string name)
        {
            var productsSummariesBatches = await _queryContext.AllProductsSummariesBatches.ToListAsync();
            var productSummaryBatchViewModel = productsSummariesBatches
                .Select(r => _mapper.Map<ProductSummaryBatchViewModel>(r))
                .Where(r => r.ProductsId == id && r.Batch.Equals(name))
                .FirstOrDefault();
            return productSummaryBatchViewModel;
        }

        public async Task<IEnumerable<ProductSummaryBatchViewModel>> GetProductSummaryBatchByProductId(Guid productsId)
        {
            var productsSummariesBatches = await _queryContext.AllProductsSummariesBatches.ToListAsync();
            var productsSummariesBatchesViewModel = productsSummariesBatches
                .Select(r => _mapper.Map<ProductSummaryBatchViewModel>(r))
                .Where(r => r.ProductsId == productsId)
                .OrderByDescending(r => r.NumberOfUnitsBatch)
                .ToList();

            return productsSummariesBatchesViewModel;
        }

        public async Task<IEnumerable<ProductSummaryBatchViewModel>> GetValidProductSummaryBatchList(Guid productsId)
        {
            var productsSummariesBatches = await _queryContext.AllProductsSummariesBatches.ToListAsync();
            var productsSummariesBatchesViewModel = productsSummariesBatches
                .Select(r => _mapper.Map<ProductSummaryBatchViewModel>(r))
                .Where(r => r.ProductsId == productsId && r.NumberOfUnitsBatch > 0 && r.ValidityBatchDate >= DateTime.Now)
                .OrderBy(r => r.ValidityBatchDate)
                .ToList();

            return productsSummariesBatchesViewModel;
        }
    }
}
