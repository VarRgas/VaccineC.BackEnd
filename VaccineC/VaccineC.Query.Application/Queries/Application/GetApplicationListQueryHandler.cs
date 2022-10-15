using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationListQueryHandler : IRequestHandler<GetApplicationListQuery, IEnumerable<ApplicationViewModel>>
    {

        private readonly IApplicationAppService _appService;

        public GetApplicationListQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ApplicationViewModel>> Handle(GetApplicationListQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllAsync();
        }
    }
}
