using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetProductSummaryBatchByIdQueryHandler : IRequestHandler<GetProductSummaryBatchByIdQuery, ProductSummaryBatchViewModel>
    {

        private readonly IMediator _mediator;

        public GetProductSummaryBatchByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ProductSummaryBatchViewModel> Handle(GetProductSummaryBatchByIdQuery request, CancellationToken cancellationToken)
        {
            var productsSummariesBatches = await _mediator.Send(new GetProductSummaryBatchListQuery());
            var productSummaryBatch = productsSummariesBatches.FirstOrDefault(psb => psb.ID == request.Id);
            return productSummaryBatch;
        }
    }
}
