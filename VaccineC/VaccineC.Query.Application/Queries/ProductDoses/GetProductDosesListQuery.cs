using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductDoses
{
    public class GetProductDosesListQuery : IRequest<IEnumerable<ProductDosesViewModel>>
    {
    }
}
