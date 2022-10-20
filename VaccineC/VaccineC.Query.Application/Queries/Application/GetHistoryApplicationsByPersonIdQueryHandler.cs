using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetHistoryApplicationsByPersonIdQueryHandler : IRequestHandler<GetHistoryApplicationsByPersonIdQuery, IEnumerable<ApplicationHistoryViewModel>>
    {

        private readonly IApplicationAppService _appService;

        public GetHistoryApplicationsByPersonIdQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ApplicationHistoryViewModel>> Handle(GetHistoryApplicationsByPersonIdQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetHistoryApplicationsByPersonId(request.PersonId);
        }
    }
}
