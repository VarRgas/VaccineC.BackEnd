using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetValidProductsSummariesBatchesByProductIdListQuery : IRequest<IEnumerable<ProductSummaryBatchViewModel>>
    {
        public Guid ProductsId { get; set; }

        public GetValidProductsSummariesBatchesByProductIdListQuery(Guid productsId)
        {
            ProductsId = productsId;
        }
    }
}
