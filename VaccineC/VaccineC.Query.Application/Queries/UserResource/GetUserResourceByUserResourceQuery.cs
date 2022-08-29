using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.UserResource
{
    public class GetUserResourceByUserResourceQuery : IRequest<UserResourceViewModel>
    {
        public Guid UsersId;
        public Guid ResourcesId;

        public GetUserResourceByUserResourceQuery(Guid usersId, Guid resourcesId)
        {
            UsersId = usersId;
            ResourcesId = resourcesId;
        }
    }
}
