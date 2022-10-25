using AutoMapper;
using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationsByParametersQueryHandler : IRequestHandler<GetApplicationsByParametersQuery, IEnumerable<PersonViewModel>>
    {
        private readonly IApplicationAppService _appService;

        public GetApplicationsByParametersQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<PersonViewModel>> Handle(GetApplicationsByParametersQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllApplicationsByParameter(request.PersonName, request.ApplicationDate, request.ResponsibleId);
        }
    }
}
