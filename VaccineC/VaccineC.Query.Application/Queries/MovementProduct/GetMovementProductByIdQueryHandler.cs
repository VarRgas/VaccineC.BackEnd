using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.MovementProduct
{
    public class GetMovementProductByIdQueryHandler : IRequestHandler<GetMovementProductByIdQuery, MovementProductViewModel>
    {

        private readonly IMediator _mediator;

        public GetMovementProductByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<MovementProductViewModel> Handle(GetMovementProductByIdQuery request, CancellationToken cancellationToken)
        {
            var movementsProducts = await _mediator.Send(new GetMovementProductListQuery());
            var movementProduct = movementsProducts.FirstOrDefault(mp => mp.ID == request.Id);
            return movementProduct;
        }
    }
}
