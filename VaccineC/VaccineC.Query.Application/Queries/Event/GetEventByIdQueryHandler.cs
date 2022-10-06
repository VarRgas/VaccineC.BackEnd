using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Event
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventViewModel>
    {
        private readonly IMediator _mediator;

        public GetEventByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<EventViewModel> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var events = await _mediator.Send(new GetEventListQuery());
            var eventClass = events.FirstOrDefault(d => d.ID == request.Id);
            return eventClass;
        }
    }
}
