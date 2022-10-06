using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Event
{
    public class GetEventByIdQuery : IRequest<EventViewModel>
    {
        public Guid Id;

        public GetEventByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
