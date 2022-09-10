using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetProductSummaryBatchByIdQuery : IRequest<ProductSummaryBatchViewModel>
    {
        public Guid Id;

        public GetProductSummaryBatchByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
