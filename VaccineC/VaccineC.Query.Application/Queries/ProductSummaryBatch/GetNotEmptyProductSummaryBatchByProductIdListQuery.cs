using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetNotEmptyProductSummaryBatchByProductIdListQuery : IRequest<IEnumerable<ProductSummaryBatchViewModel>>
    {
        public Guid ProductsId { get; set; }

        public GetNotEmptyProductSummaryBatchByProductIdListQuery(Guid productsId)
        {
            ProductsId = productsId;
        }
    }
}
