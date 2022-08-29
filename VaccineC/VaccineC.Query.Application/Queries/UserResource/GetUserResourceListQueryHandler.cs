using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.UserResource
{
    public class GetUserResourceListQueryHandler : IRequestHandler<GetUserResourceListQuery, IEnumerable<UserResourceViewModel>>
    {

        private readonly IUserResourceAppService _userResourceAppService;

        public GetUserResourceListQueryHandler(IUserResourceAppService userResourceAppService) {
            _userResourceAppService = userResourceAppService;
        }

        public async Task<IEnumerable<UserResourceViewModel>> Handle(GetUserResourceListQuery request, CancellationToken cancellationToken)
        {
            return await _userResourceAppService.GetAllAsync();
        }
    }
}
