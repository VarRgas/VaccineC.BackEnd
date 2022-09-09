using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Product
{
    public class GetProductListQuery : IRequest<IEnumerable<ProductViewModel>>
    {
    }
}
