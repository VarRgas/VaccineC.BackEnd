using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.User
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, IEnumerable<UserViewModel>>
    {
        private readonly IUserAppService _userAppService;

        public GetUserByEmailQueryHandler(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<IEnumerable<UserViewModel>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _userAppService.GetByEmail(request.Email);
        }
    }
}
