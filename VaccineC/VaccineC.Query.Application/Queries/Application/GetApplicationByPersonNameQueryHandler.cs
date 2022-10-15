using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationByPersonNameQueryHandler : IRequestHandler<GetApplicationByPersonNameQuery, IEnumerable<ApplicationViewModel>>
    {
        private readonly IApplicationAppService _appService;

        public GetApplicationByPersonNameQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ApplicationViewModel>> Handle(GetApplicationByPersonNameQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetByName(request.Name);
        }

    }
}
