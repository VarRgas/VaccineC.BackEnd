using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.User
{
    public class GetUserPermissionQueryHandler : IRequestHandler<GetUserPermissionQuery, Boolean>
    {

        private readonly IUserAppService _userAppService;

        public GetUserPermissionQueryHandler(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<Boolean> Handle(GetUserPermissionQuery request, CancellationToken cancellationToken)
        {      
            return await _userAppService.GetUserPermission(request.Id, request.CurrentUrl);
        }
    }
}
