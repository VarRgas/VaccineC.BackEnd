using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationsByProductIdQueryHandler : IRequestHandler<GetApplicationsByProductIdQuery, IEnumerable<ApplicationProductViewModel>>
    {
        private readonly IApplicationAppService _appService;

        public GetApplicationsByProductIdQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ApplicationProductViewModel>> Handle(GetApplicationsByProductIdQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetApplicationsByProductId(request.Month, request.Year);
        }
    }
}
