using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.UserResource
{
    public class GetUserResourceListMenuByUserQuery : IRequest<UserResourceMenuViewModel>
    {
        public Guid UserId { get; set; }

        public GetUserResourceListMenuByUserQuery(Guid userId)
        {
            UserId = userId;
        }
    
    }
}
