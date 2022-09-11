using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.MovementProduct
{
    public class GetMovementProductListByMovementQuery : IRequest<IEnumerable<MovementProductViewModel>>
    {
        public Guid MovementId;

        public GetMovementProductListByMovementQuery(Guid movementId)
        {
            MovementId = movementId;
        }
    }
}
