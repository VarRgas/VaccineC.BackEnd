using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetAuthorizationForApplicationQueryHandler : IRequestHandler<GetAuthorizationForApplicationQuery, IEnumerable<AuthorizationViewModel>>
    {

        private readonly IAuthorizationAppService _appService;

        public GetAuthorizationForApplicationQueryHandler(IAuthorizationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<AuthorizationViewModel>> Handle(GetAuthorizationForApplicationQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllForApplication();
        }
    }
}
