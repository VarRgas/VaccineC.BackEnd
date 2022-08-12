using MediatR;
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
            var user = _context.Users.FirstOrDefault(a => a.Email == request.Email);

            if (user == null || !PasswordManager.ValidatePassword(request.Password, user.Password))
                throw new Exception("Usuário ou senha inválidos, verifique e tente novamente!");

            return new LoginViewModel()
            {
                ID = user.ID,
                Email = user.Email,
                Token = ""
            };
        }
    }
}
