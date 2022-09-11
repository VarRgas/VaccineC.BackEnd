using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.MovementProduct
{
    public class GetMovementProductListQuery : IRequest<IEnumerable<MovementProductViewModel>>
    {
    }
}
