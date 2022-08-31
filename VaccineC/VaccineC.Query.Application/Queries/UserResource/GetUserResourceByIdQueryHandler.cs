using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.UserResource
{
    public class GetUserResourceByIdQueryHandler : IRequestHandler<GetUserResourceByIdQuery, UserResourceViewModel>
    {

        private readonly IMediator _mediator;

        public GetUserResourceByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UserResourceViewModel> Handle(GetUserResourceByIdQuery request, CancellationToken cancellationToken)
        {
            var usersResources = await _mediator.Send(new GetUserResourceListQuery());
            var userResource = usersResources.FirstOrDefault(ur => ur.ID == request.Id);
            return userResource;
        }
    }
}
