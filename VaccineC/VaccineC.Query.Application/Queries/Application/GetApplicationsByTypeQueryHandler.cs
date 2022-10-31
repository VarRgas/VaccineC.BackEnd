using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationsByTypeQueryHandler : IRequestHandler<GetApplicationsByTypeQuery, IEnumerable<ApplicationTypeViewModel>>
    {
        private readonly IApplicationAppService _appService;

        public GetApplicationsByTypeQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ApplicationTypeViewModel>> Handle(GetApplicationsByTypeQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetApplicationsByType(request.Month, request.Year);
        }
    }
}
