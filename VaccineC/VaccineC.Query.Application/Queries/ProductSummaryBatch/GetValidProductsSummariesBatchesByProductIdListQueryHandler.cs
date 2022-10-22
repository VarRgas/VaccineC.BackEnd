using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetValidProductsSummariesBatchesByProductIdListQueryHandler : IRequestHandler<GetValidProductsSummariesBatchesByProductIdListQuery, IEnumerable<ProductSummaryBatchViewModel>>
    {
        private readonly IProductSummaryBatchAppService _productSummaryBatchAppService;

        public GetValidProductsSummariesBatchesByProductIdListQueryHandler(IProductSummaryBatchAppService productSummaryBatchAppService)
        {
            _productSummaryBatchAppService = productSummaryBatchAppService;
        }

        public async Task<IEnumerable<ProductSummaryBatchViewModel>> Handle(GetValidProductsSummariesBatchesByProductIdListQuery request, CancellationToken cancellationToken)
        {
            return await _productSummaryBatchAppService.GetValidProductsSummariesBatchesByProductId(request.ProductsId);
        }
    }
}
