using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Movement
{
    public class GetMovementByIdQueryHandler : IRequestHandler<GetMovementByIdQuery, MovementViewModel>
    {

        private readonly IMediator _mediator;

        public GetMovementByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<MovementViewModel> Handle(GetMovementByIdQuery request, CancellationToken cancellationToken)
        {
            var movements = await _mediator.Send(new GetMovementListQuery());
            var movement = movements.FirstOrDefault(m => m.ID == request.Id);
            return movement;
        }
    }
}
