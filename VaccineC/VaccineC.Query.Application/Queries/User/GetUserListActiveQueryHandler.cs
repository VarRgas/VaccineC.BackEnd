using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.User
{
    public class GetUserListActiveQueryHandler : IRequestHandler<GetUserListActiveQuery, IEnumerable<UserViewModel>>
    {

        private readonly IUserAppService _userAppService;

        public GetUserListActiveQueryHandler(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<IEnumerable<UserViewModel>> Handle(GetUserListActiveQuery request, CancellationToken cancellationToken)
        {
            return await _userAppService.GetAllActive();
        }
    }
}
