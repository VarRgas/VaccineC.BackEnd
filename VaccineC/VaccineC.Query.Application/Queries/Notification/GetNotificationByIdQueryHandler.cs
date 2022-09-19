using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Notification
{
    public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, NotificationViewModel>
    {
        private readonly IMediator _mediator;

        public GetNotificationByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<NotificationViewModel> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _mediator.Send(new GetNotificationListQuery());
            var notification = notifications.FirstOrDefault(pf => pf.ID == request.Id);
            return notification;
        }
    }
}
