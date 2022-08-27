using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.User
{
    public class GetUserByEmailQuery : IRequest<IEnumerable<UserViewModel>>
    {
        public string Email { get; set; }

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
