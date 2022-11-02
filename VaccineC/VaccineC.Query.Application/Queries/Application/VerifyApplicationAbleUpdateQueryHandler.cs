using AutoMapper;
using MediatR;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;


namespace VaccineC.Query.Application.Queries.Application
{
    public class VerifyApplicationAbleUpdateQueryHandler : IRequestHandler<VerifyApplicationAbleUpdateQuery, Boolean>
    {

        private readonly IApplicationAppService _appService;

        public VerifyApplicationAbleUpdateQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<bool> Handle(VerifyApplicationAbleUpdateQuery request, CancellationToken cancellationToken)
        {
            return await _appService.VerifyApplicationAbleUpdate(request.ApplicationId, request.UserId);
        }
    }
}
