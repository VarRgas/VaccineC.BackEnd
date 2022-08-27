using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.User
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IEnumerable<UserViewModel>>
    {

        private readonly IUserAppService _userAppService;

        public GetUserListQueryHandler(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<IEnumerable<UserViewModel>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return await _userAppService.GetAllAsync(); ;
        }
    }
}
