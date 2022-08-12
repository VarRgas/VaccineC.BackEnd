using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;

namespace VaccineC.Query.Application.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginViewModel>
    {
        private readonly ILoginAppService _loginAppService;
        private readonly VaccineCContext _context;
        public LoginQueryHandler(ILoginAppService loginAppService, VaccineCContext context)
        {
            _loginAppService = loginAppService;
            _context = context;
        }

        public async Task<LoginViewModel> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(a => a.Email == request.Email);

            if (user.Password != request.Password)
                throw new Exception("Usuario o senha invalido");

            return new LoginViewModel()
            {
                Email = user.Email,
                Token = "h4r87yrg388t3tb3487trg"

            };
        }
    }
}
