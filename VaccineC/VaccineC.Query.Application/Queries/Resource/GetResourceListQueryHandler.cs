using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Resource
{
    public class GetResourceListQueryHandler : IRequestHandler<GetResourceListQuery, IEnumerable<ResourceViewModel>>
    {

        private readonly IResourceAppService _resourceAppService;

        public GetResourceListQueryHandler(IResourceAppService resourceAppService)
        {
            _resourceAppService = resourceAppService;
        }

        public async Task<IEnumerable<ResourceViewModel>> Handle(GetResourceListQuery request, CancellationToken cancellationToken)
        {
            return await _resourceAppService.GetAllAsync();
        }
    }
}
