using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetProductSummaryBatchByProductIdQueryHandler : IRequestHandler<GetProductSummaryBatchByProductIdQuery, IEnumerable<ProductSummaryBatchViewModel>>
    {
        private readonly IProductSummaryBatchAppService _productSummaryBatchAppService;

        public GetProductSummaryBatchByProductIdQueryHandler(IProductSummaryBatchAppService productSummaryBatchAppService)
        {
            _productSummaryBatchAppService = productSummaryBatchAppService;
        }

        public async Task<IEnumerable<ProductSummaryBatchViewModel>> Handle(GetProductSummaryBatchByProductIdQuery request, CancellationToken cancellationToken)
        {
            return await _productSummaryBatchAppService.GetProductSummaryBatchByProductId(request.ProductsId);
        }
    }
}
