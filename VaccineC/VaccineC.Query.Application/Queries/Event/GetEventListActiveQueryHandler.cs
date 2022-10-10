using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Event
{
    public class GetEventListActiveQueryHandler : IRequestHandler<GetEventListActiveQuery, IEnumerable<EventViewModel>>
    {

        private readonly IEventAppService _eventAppService;

        public GetEventListActiveQueryHandler(IEventAppService eventAppService)
        {
            _eventAppService = eventAppService;
        }

        public async Task<IEnumerable<EventViewModel>> Handle(GetEventListActiveQuery request, CancellationToken cancellationToken)
        {
            return await _eventAppService.GetAllActive();
        }
    }
}
