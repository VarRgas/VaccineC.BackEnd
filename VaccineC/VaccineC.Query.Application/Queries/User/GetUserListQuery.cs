using MediatR;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.User
{
    public class GetUserListQuery : IRequest<IEnumerable<UserViewModel>>
    {
    }
}
