using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.UserResource
{
    public class GetUserResourceListByUserQuery : IRequest<IEnumerable<UserResourceViewModel>>
    {
        public Guid UsersId { get; set; }

        public GetUserResourceListByUserQuery(Guid usersId)
        {
            UsersId = usersId;
        }
    }
}
