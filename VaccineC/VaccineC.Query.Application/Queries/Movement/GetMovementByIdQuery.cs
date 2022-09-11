using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Movement
{
    public class GetMovementByIdQuery : IRequest<MovementViewModel>
    {
        public Guid Id;

        public GetMovementByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
