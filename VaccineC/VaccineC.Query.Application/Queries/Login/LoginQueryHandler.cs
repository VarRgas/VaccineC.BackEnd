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

            var usersResources = await _queryContext.AllUserResources.ToListAsync();
            var userResourceViewModel = usersResources
                .Select(ur => _mapper.Map<UserResourceViewModel>(ur))
                .Where(ur => ur.UsersId.Equals(user.ID) && ur.ResourcesId.ToString().Equals("9D027829-002B-460B-9E8D-31FADB3FF313", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            string showNotification = "";

            if (userResourceViewModel != null) 
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
