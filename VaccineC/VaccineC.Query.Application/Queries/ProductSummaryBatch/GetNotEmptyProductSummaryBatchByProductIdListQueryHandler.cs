using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetNotEmptyProductSummaryBatchByProductIdListQueryHandler : IRequestHandler<GetNotEmptyProductSummaryBatchByProductIdListQuery, IEnumerable<ProductSummaryBatchViewModel>>
    {
        private readonly IProductSummaryBatchAppService _productSummaryBatchAppService;

        public GetNotEmptyProductSummaryBatchByProductIdListQueryHandler(IProductSummaryBatchAppService productSummaryBatchAppService)
        {
            _productSummaryBatchAppService = productSummaryBatchAppService;
        }

        public async Task<IEnumerable<ProductSummaryBatchViewModel>> Handle(GetNotEmptyProductSummaryBatchByProductIdListQuery request, CancellationToken cancellationToken)
        {
            return await _productSummaryBatchAppService.GetNotEmptyProductsSummaryBatchListByProductId(request.ProductsId);
        }
    }
}
