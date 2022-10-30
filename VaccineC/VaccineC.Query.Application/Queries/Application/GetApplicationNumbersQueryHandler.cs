using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationNumbersQueryHandler : IRequestHandler<GetApplicationNumbersQuery, ApplicationNumberViewModel>
    {
        private readonly IApplicationAppService _appService;

        public GetApplicationNumbersQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<ApplicationNumberViewModel> Handle(GetApplicationNumbersQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetApplicationsNumbers(request.Month, request.Year);
        }
    }
}
