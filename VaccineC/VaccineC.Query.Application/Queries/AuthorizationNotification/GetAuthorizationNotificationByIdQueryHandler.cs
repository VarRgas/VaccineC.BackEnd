using AutoMapper;
using MediatR;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Query.Application.Queries.AuthorizationNotification
{
    public class GetAuthorizationNotificationByIdQueryHandler : IRequestHandler<GetAuthorizationNotificationByIdQuery, AuthorizationNotificationViewModel>
    {

        private readonly IMediator _mediator;

        public GetAuthorizationNotificationByIdQueryHandler(IMediator mediator, IMapper mapper, VaccineCContext context)
        {
            _mediator = mediator;
        }

        public async Task<AuthorizationNotificationViewModel> Handle(GetAuthorizationNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var authorizationsNotifications = await _mediator.Send(new GetAuthorizationNotificationListQuery());
            var authorizationNotification = authorizationsNotifications.FirstOrDefault(a => a.ID == request.Id);
           
            return authorizationNotification;
        }

    }
}
