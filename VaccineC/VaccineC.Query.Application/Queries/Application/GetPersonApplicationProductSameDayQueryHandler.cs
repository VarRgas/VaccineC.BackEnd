using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetPersonApplicationProductSameDayQueryHandler : IRequestHandler<GetPersonApplicationProductSameDayQuery, Boolean>
    {

        private readonly IApplicationAppService _appService;

        public GetPersonApplicationProductSameDayQueryHandler(IApplicationAppService appService)
        {
            _appService = appService;
        }

        public async Task<bool> Handle(GetPersonApplicationProductSameDayQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetPersonApplicationProductSameDay(request.PersonId, request.ProductId);
        }
    }
}
