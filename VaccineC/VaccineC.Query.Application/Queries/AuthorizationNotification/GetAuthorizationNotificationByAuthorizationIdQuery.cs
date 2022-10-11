using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.AuthorizationNotification
{
    public class GetAuthorizationNotificationByAuthorizationIdQuery : IRequest<AuthorizationNotificationViewModel>
    {
        public Guid AuthorizationId;

        public GetAuthorizationNotificationByAuthorizationIdQuery(Guid authorizationId)
        {
            AuthorizationId = authorizationId;
        }
    }
}
