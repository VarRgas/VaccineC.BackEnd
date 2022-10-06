using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Event
{
    public class GetEventListQueryHandler : IRequestHandler<GetEventListQuery, IEnumerable<EventViewModel>>
    {

        private readonly IEventAppService _eventAppService;

        public GetEventListQueryHandler(IEventAppService eventAppService)
        {
            _eventAppService = eventAppService;
        }

        public async Task<IEnumerable<EventViewModel>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
        {
            return await _eventAppService.GetAllAsync();
        }
    }
}
