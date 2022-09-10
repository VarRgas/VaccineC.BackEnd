using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetProductSummaryBatchListQueryHandler : IRequestHandler<GetProductSummaryBatchListQuery, IEnumerable<ProductSummaryBatchViewModel>>
    {
        private readonly IProductSummaryBatchAppService _productSummaryBatchAppService;

        public GetProductSummaryBatchListQueryHandler(IProductSummaryBatchAppService productSummaryBatchAppService)
        {
            _productSummaryBatchAppService = productSummaryBatchAppService;
        }
        public async Task<IEnumerable<ProductSummaryBatchViewModel>> Handle(GetProductSummaryBatchListQuery request, CancellationToken cancellationToken)
        {
            return await _productSummaryBatchAppService.GetAllAsync();
        }
    }
}
