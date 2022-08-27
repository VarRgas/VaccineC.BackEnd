using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.User
{
    public class GetUserListActiveQuery : IRequest<IEnumerable<UserViewModel>>
    {
    }
}
