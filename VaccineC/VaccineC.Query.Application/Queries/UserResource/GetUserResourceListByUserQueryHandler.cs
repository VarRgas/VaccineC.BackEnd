using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.UserResource
{
    public class GetUserResourceListByUserQueryHandler : IRequestHandler<GetUserResourceListByUserQuery, IEnumerable<UserResourceViewModel>>
    {

        private readonly IUserResourceAppService _userResourceAppService;

        public GetUserResourceListByUserQueryHandler(IUserResourceAppService userResourceAppService)
        {
            _userResourceAppService = userResourceAppService;
        }

        public async Task<IEnumerable<UserResourceViewModel>> Handle(GetUserResourceListByUserQuery request, CancellationToken cancellationToken)
        {
            return await _userResourceAppService.GetAllByUser(request.UsersId);
        }
    }
}
