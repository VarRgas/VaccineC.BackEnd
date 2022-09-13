using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetProductSummaryBatchByNameQueryHandler : IRequestHandler<GetProductSummaryBatchByNameQuery, ProductSummaryBatchViewModel>
    {
        private readonly IProductSummaryBatchAppService _productSummaryBatchAppService;

        public GetProductSummaryBatchByNameQueryHandler(IProductSummaryBatchAppService productSummaryBatchAppService)
        {
            _productSummaryBatchAppService = productSummaryBatchAppService;
        }

        public async Task<ProductSummaryBatchViewModel> Handle(GetProductSummaryBatchByNameQuery request, CancellationToken cancellationToken)
        {
            return await _productSummaryBatchAppService.GetByName(request.Id, request.Name);
        }
    }
}
