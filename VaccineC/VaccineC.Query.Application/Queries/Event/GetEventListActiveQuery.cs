using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Event
{
    public class GetEventListActiveQuery : IRequest<IEnumerable<EventViewModel>>
    {
    }
}
