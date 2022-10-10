using AutoMapper;
using MediatR;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetAuthorizationByIdQueryHandler : IRequestHandler<GetAuthorizationByIdQuery, AuthorizationViewModel>
    {

        private readonly IMediator _mediator;

        public GetAuthorizationByIdQueryHandler(IMediator mediator, IMapper mapper, VaccineCContext context)
        {
            _mediator = mediator;
        }

        public async Task<AuthorizationViewModel> Handle(GetAuthorizationByIdQuery request, CancellationToken cancellationToken)
        {
            var authorizations = await _mediator.Send(new GetAuthorizationListQuery());
            var authorization = authorizations.FirstOrDefault(a => a.ID == request.Id);
            return authorization;
        }
    }
}
