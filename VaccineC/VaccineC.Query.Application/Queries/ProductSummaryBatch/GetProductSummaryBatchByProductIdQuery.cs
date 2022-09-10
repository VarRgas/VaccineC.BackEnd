using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetProductSummaryBatchByProductIdQuery : IRequest<IEnumerable<ProductSummaryBatchViewModel>>
    {
        public Guid ProductsId { get; set; }

        public GetProductSummaryBatchByProductIdQuery(Guid productsId)
        {
            ProductsId = productsId;
        }
    }
}
