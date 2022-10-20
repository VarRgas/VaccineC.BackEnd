using AutoMapper;
using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationNumberByPersonIdQueryHandler : IRequestHandler<GetApplicationNumberByPersonIdQuery, int>
    {
        private readonly IApplicationAppService _appService;

        public GetApplicationNumberByPersonIdQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<int> Handle(GetApplicationNumberByPersonIdQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetApplicationNumberByPersonId(request.PersonId);
        }
    }
}
