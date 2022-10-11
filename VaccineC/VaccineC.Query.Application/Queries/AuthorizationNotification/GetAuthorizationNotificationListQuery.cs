using MediatR;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.AuthorizationNotification
{
    public class GetAuthorizationNotificationListQuery : IRequest<IEnumerable<AuthorizationNotificationViewModel>>
    {
    }
}
