using AutoMapper;
using MediatR;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Query.Application.Queries.AuthorizationNotification
{
    public class GetAuthorizationNotificationByAuthorizationIdQueryHandler : IRequestHandler<GetAuthorizationNotificationByAuthorizationIdQuery, AuthorizationNotificationViewModel>
    {

        private readonly IMediator _mediator;

        public GetAuthorizationNotificationByAuthorizationIdQueryHandler(IMediator mediator, IMapper mapper, VaccineCContext context)
        {
            _mediator = mediator;
        }

        public async Task<AuthorizationNotificationViewModel> Handle(GetAuthorizationNotificationByAuthorizationIdQuery request, CancellationToken cancellationToken)
        {
            var authorizationsNotifications = await _mediator.Send(new GetAuthorizationNotificationListQuery());
            var authorizationNotification = authorizationsNotifications.FirstOrDefault(a => a.AuthorizationId == request.AuthorizationId);
            return authorizationNotification;
        }
    }
}
