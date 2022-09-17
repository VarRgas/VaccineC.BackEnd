using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetValidProductSummaryBatchListQuery : IRequest<IEnumerable<ProductSummaryBatchViewModel>>
    {
        public Guid ProductsId { get; set; }

        public GetValidProductSummaryBatchListQuery(Guid productsId)
        {
            ProductsId = productsId;
        }
    }
}
