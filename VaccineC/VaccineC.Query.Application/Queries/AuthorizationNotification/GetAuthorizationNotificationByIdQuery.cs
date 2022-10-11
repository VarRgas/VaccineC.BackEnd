using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.AuthorizationNotification
{
    public class GetAuthorizationNotificationByIdQuery : IRequest<AuthorizationNotificationViewModel>
    {
        public Guid Id;

        public GetAuthorizationNotificationByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
