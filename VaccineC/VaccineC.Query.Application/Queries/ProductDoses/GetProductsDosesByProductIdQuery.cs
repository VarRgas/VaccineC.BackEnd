using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductDoses
{
    public class GetProductsDosesByProductIdQuery : IRequest<IEnumerable<ProductDosesViewModel>>
    {
        public Guid ProductsId { get; set; }

        public GetProductsDosesByProductIdQuery(Guid productsId)
        {
            ProductsId = productsId;
        }
    }
}
