using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetAuthorizationByParameterQueryHandler : IRequestHandler<GetAuthorizationByParameterQuery, IEnumerable<AuthorizationViewModel>>
    {

        private readonly IAuthorizationAppService _appService;

        public GetAuthorizationByParameterQueryHandler(IAuthorizationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<AuthorizationViewModel>> Handle(GetAuthorizationByParameterQuery request, CancellationToken cancellationToken)
        {

            long n;
            bool isNumeric = long.TryParse(request.Information, out n);

            if (isNumeric)
            {
                int authNumber = int.Parse(request.Information);
                return await _appService.GetAllByAuthNumber(authNumber, request.Situation, request.ResponsibleId);
            }
            else
            {
                return await _appService.GetAllByBorrowerName(request.Information, request.Situation, request.ResponsibleId);

            }
        }
    }
}
