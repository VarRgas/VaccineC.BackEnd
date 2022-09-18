using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetAuthorizationByIdQueryHandler : IRequestHandler<GetAuthorizationByIdQuery, AuthorizationViewModel>
    {

        private readonly IMediator _mediator;

        public GetAuthorizationByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<AuthorizationViewModel> Handle(GetAuthorizationByIdQuery request, CancellationToken cancellationToken)
        {
            var authorizations = await _mediator.Send(new GetAuthorizationListQuery());
            var authorization = authorizations.FirstOrDefault(bp => bp.ID == request.Id);
            return authorization;
        }
    }
}
