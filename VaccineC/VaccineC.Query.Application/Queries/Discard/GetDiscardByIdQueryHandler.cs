using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Discard
{
    public class GetDiscardByIdQueryHandler : IRequestHandler<GetDiscardByIdQuery, DiscardViewModel>
    {
        private readonly IMediator _mediator;

        public GetDiscardByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<DiscardViewModel> Handle(GetDiscardByIdQuery request, CancellationToken cancellationToken)
        {
            var discards = await _mediator.Send(new GetDiscardListQuery());
            var discard = discards.FirstOrDefault(d => d.ID == request.Id);
            return discard;
        }
    }
}
