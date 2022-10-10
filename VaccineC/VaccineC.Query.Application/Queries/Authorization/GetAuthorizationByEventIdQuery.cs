using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetAuthorizationByEventIdQuery : IRequest<AuthorizationViewModel>
    {
        public Guid EventId;

        public GetAuthorizationByEventIdQuery(Guid eventId)
        {
            EventId = eventId;
        }
    }
}
