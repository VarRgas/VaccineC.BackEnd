using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.MovementProduct
{
    public class GetMovementProductByIdQuery : IRequest<MovementProductViewModel>
    {
        public Guid Id;

        public GetMovementProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
