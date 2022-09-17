using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetNotEmptyProductSummaryBatchListQuery : IRequest<IEnumerable<ProductSummaryBatchViewModel>>
    {
    }
}
