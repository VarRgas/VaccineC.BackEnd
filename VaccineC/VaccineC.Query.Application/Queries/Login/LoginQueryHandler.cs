using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;
using VaccineC.Security;

namespace VaccineC.Query.Application.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginViewModel>
    {
        private readonly VaccineCContext _context;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public LoginQueryHandler(VaccineCContext context, IQueryContext queryContext, IMapper mapper)
        {
            _context = context;
            _queryContext = queryContext;
            _mapper = mapper;   
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

            int userValidId = (from u in _context.UsersResources
                                      join r in _context.Resources on u.ResourcesId equals r.ID
                                      where r.Name.Equals("SITUAÇÃO ESTOQUE")
                                      && u.UsersId.Equals(user.ID)
                                      select u).Count();

            string showNotification = "";

            if (userValidId > 0) 
            {
                showNotification = "S";
            }
            else
            {
                showNotification = "N";
            }

            return new LoginViewModel()
            {
                ID = user.ID,
                Email = user.Email,
                PersonID = user.PersonID,
                PersonName = user.Name,
                PersonProfilePic = user.ProfilePic,
                Token = "",
                ShowNotification = showNotification
            };

        }
    }
}
