using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Login
{
    public class LoginQuery : IRequest<LoginViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}