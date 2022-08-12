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

            if (user.Password != request.Password)
                throw new Exception("Usuario o senha invalido");

            var hashedPassword = PasswordManager.HashPassword(request.Password);
            var tokenIsValid = PasswordManager.ValidatePassword(hashedPassword, request.Password);

            if (!tokenIsValid)
                throw new Exception("Token invalido!");

            return new LoginViewModel()
            {
                Email = user.Email,
                Token = hashedPassword
            };
        }
    }
}
