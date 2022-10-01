using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Discard
{
    public class GetDiscardListQueryHandler : IRequestHandler<GetDiscardListQuery, IEnumerable<DiscardViewModel>>
    {

        private readonly IDiscardAppService _discardAppService;

        public GetDiscardListQueryHandler(IDiscardAppService discardAppService)
        {
            _discardAppService = discardAppService;
        }

        public async Task<IEnumerable<DiscardViewModel>> Handle(GetDiscardListQuery request, CancellationToken cancellationToken)
        {
            return await _discardAppService.GetAllAsync();
        }
    }
}
