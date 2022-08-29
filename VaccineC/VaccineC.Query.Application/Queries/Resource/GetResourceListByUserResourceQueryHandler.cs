using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Resource
{
    public class GetResourceListByUserResourceQueryHandler : IRequestHandler<GetResourceListByUserResourceQuery, IEnumerable<ResourceViewModel>>
    {
        private readonly IResourceAppService _resourceAppService;

        public GetResourceListByUserResourceQueryHandler(IResourceAppService resourceAppService)
        {
            _resourceAppService = resourceAppService;
        }

        public async Task<IEnumerable<ResourceViewModel>> Handle(GetResourceListByUserResourceQuery request, CancellationToken cancellationToken)
        {
            return await _resourceAppService.GetByUserResource(request.UserId);
        }
    }
}
