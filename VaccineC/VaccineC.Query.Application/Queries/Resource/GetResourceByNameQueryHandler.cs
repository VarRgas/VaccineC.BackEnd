using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Resource
{
    public class GetResourceByNameQueryHandler : IRequestHandler<GetResourceByNameQuery, IEnumerable<ResourceViewModel>>
    {
        private readonly IResourceAppService _resourceAppService;

        public GetResourceByNameQueryHandler(IResourceAppService resourceAppService)
        {
            _resourceAppService = resourceAppService;
        }

        public async Task<IEnumerable<ResourceViewModel>> Handle(GetResourceByNameQuery request, CancellationToken cancellationToken)
        {
            return await _resourceAppService.GetByName(request.Name);
        }

    }
}
