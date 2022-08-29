using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.UserResource
{
    public class GetUserResourceListQuery : IRequest<IEnumerable<UserResourceViewModel>>
    {
    }
}
