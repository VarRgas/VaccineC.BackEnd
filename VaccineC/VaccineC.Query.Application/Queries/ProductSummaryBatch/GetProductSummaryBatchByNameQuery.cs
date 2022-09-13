using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductSummaryBatch
{
    public class GetProductSummaryBatchByNameQuery : IRequest<ProductSummaryBatchViewModel>
    {
        public Guid Id;
        public string Name;

        public GetProductSummaryBatchByNameQuery(Guid id, string name)
        {
            Id = id;
            Name = name;    
        }
    }
}
