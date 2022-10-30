using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationsByPersonGenderQueryHandler : IRequestHandler<GetApplicationsByPersonGenderQuery, IEnumerable<ApplicationPersonGenderViewModel>>
    {
        private readonly IApplicationAppService _appService;

        public GetApplicationsByPersonGenderQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ApplicationPersonGenderViewModel>> Handle(GetApplicationsByPersonGenderQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetApplicationsByPersonGender(request.Month, request.Year);
        }
    }
}
