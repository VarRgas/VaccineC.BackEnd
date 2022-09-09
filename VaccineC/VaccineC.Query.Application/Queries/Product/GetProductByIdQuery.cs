using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Product
{
    public class GetProductByIdQuery : IRequest<ProductViewModel>
    {
        public Guid Id;

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
