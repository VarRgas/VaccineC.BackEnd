using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Movement
{
    public class GetMovementListQuery : IRequest<IEnumerable<MovementViewModel>>
    {
    }
}
