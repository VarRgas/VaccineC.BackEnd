using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetProductSummaryBatchBelowMinimumStockListQueryHandler : IRequestHandler<GetProductSummaryBatchBelowMinimumStockListQuery, IEnumerable<ProductBelowMinimumViewModel>>
    {
        private readonly IProductSummaryBatchAppService _productSummaryBatchAppService;

        public GetProductSummaryBatchBelowMinimumStockListQueryHandler(IProductSummaryBatchAppService productSummaryBatchAppService)
        {
            _productSummaryBatchAppService = productSummaryBatchAppService;
        }
        public async Task<IEnumerable<ProductBelowMinimumViewModel>> Handle(GetProductSummaryBatchBelowMinimumStockListQuery request, CancellationToken cancellationToken)
        {
            return await _productSummaryBatchAppService.GetAllBelowMinimumStockAsync();
        }
    }
}
