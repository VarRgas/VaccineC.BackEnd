using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetValidProductSummaryBatchListQueryHandler : IRequestHandler<GetValidProductSummaryBatchListQuery, IEnumerable<ProductSummaryBatchViewModel>>
    {
        private readonly IProductSummaryBatchAppService _productSummaryBatchAppService;

        public GetValidProductSummaryBatchListQueryHandler(IProductSummaryBatchAppService productSummaryBatchAppService)
        {
            _productSummaryBatchAppService = productSummaryBatchAppService;
        }

        public async Task<IEnumerable<ProductSummaryBatchViewModel>> Handle(GetValidProductSummaryBatchListQuery request, CancellationToken cancellationToken)
        {
            return await _productSummaryBatchAppService.GetValidProductSummaryBatchList(request.ProductsId);
        }
    }
}
