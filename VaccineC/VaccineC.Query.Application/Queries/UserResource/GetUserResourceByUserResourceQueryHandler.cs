using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.UserResource
{
    public class GetUserResourceByUserResourceQueryHandler : IRequestHandler<GetUserResourceByUserResourceQuery, UserResourceViewModel>
    {

        private readonly IMediator _mediator;

        public GetUserResourceByUserResourceQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UserResourceViewModel> Handle(GetUserResourceByUserResourceQuery request, CancellationToken cancellationToken)
        {
            var usersResources = await _mediator.Send(new GetUserResourceListQuery());
            var userResource = usersResources.FirstOrDefault(ur => ur.UsersId == request.UsersId && ur.ResourcesId == request.ResourcesId);
            return userResource;
        }
    }
}
