using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetProductSummaryBatchBelowMinimumStockListQueryHandler : IRequestHandler<GetProductSummaryBatchBelowMinimumStockListQuery, IEnumerable<ProductSummaryBatchViewModel>>
    {
        private readonly IProductSummaryBatchAppService _productSummaryBatchAppService;

        public GetProductSummaryBatchBelowMinimumStockListQueryHandler(IProductSummaryBatchAppService productSummaryBatchAppService)
        {
            _productSummaryBatchAppService = productSummaryBatchAppService;
        }
        public async Task<IEnumerable<ProductSummaryBatchViewModel>> Handle(GetProductSummaryBatchBelowMinimumStockListQuery request, CancellationToken cancellationToken)
        {
            return await _productSummaryBatchAppService.GetAllBelowMinimumStockAsync();
        }
    }
}
