using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetNotEmptyProductSummaryBatchListQueryHandler : IRequestHandler<GetNotEmptyProductSummaryBatchListQuery, IEnumerable<ProductSummaryBatchViewModel>>
    {
        private readonly IProductSummaryBatchAppService _productSummaryBatchAppService;

        public GetNotEmptyProductSummaryBatchListQueryHandler(IProductSummaryBatchAppService productSummaryBatchAppService)
        {
            _productSummaryBatchAppService = productSummaryBatchAppService;
        }

        public async Task<IEnumerable<ProductSummaryBatchViewModel>> Handle(GetNotEmptyProductSummaryBatchListQuery request, CancellationToken cancellationToken)
        {
            return await _productSummaryBatchAppService.GetNotEmptyProductSummaryBatchList();
        }
    }
}
