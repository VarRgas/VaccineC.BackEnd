using MediatR;
using Microsoft.Data.SqlClient;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Security;

namespace VaccineC.Query.Application.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginViewModel>
    {
        private readonly VaccineCContext _context;
        public LoginQueryHandler(VaccineCContext context)
        {
            _context = context;
        }

        public async Task<LoginViewModel> Handle(LoginQuery request, CancellationToken cancellationToken)
        {

            var user = _context.Users
            .Join(_context.Persons, u => u.PersonId, p => p.ID, (u, p) => new
            {
                ID = u.ID,
                Email = u.Email,
                Password = u.Password,
                Situation = u.Situation,
                PersonID = p.ID,
                Name = p.Name,
                ProfilePic = p.ProfilePic
            })
            .Where(u => u.Situation.Equals("A"))
            .FirstOrDefault(u => u.Email == request.Email);

            if (user == null || !PasswordManager.ValidatePassword(request.Password, user.Password))
            {
                throw new ArgumentException("Usuário ou senha inválidos, verifique e tente novamente!");
            }

            return new LoginViewModel()
            {
                ID = user.ID,
                Email = user.Email,
                PersonID = user.PersonID,
                PersonName = user.Name,
                PersonProfilePic = user.ProfilePic,
                Token = ""
            };

        }
    }
}
