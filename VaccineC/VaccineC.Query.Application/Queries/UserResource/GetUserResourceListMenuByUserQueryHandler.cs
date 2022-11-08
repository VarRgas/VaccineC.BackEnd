using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.UserResource
{
    public class GetUserResourceListMenuByUserQueryHandler : IRequestHandler<GetUserResourceListMenuByUserQuery, UserResourceMenuViewModel>
    {

        private readonly IUserResourceAppService _userResourceAppService;

        public GetUserResourceListMenuByUserQueryHandler(IUserResourceAppService userResourceAppService)
        {
            _userResourceAppService = userResourceAppService;
        }

        public async Task<UserResourceMenuViewModel> Handle(GetUserResourceListMenuByUserQuery request, CancellationToken cancellationToken)
        {
            return await _userResourceAppService.GetUserResourceMenyByUser(request.UserId);
        }
    }
}
