using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;


namespace VaccineC.Query.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public UserAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var users = await _queryContext.AllUsers.ToListAsync();
            var usersViewModel = users.Select(u => _mapper.Map<UserViewModel>(u)).ToList();
            return usersViewModel;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllActive()
        {
            var users = await _queryContext.AllUsers.ToListAsync();
            var usersViewModel = users
                .Select(u => _mapper.Map<UserViewModel>(u))
                .Where(u => u.Situation.Equals("A"))
                .ToList();
            return usersViewModel;
        }

        public async Task<IEnumerable<UserViewModel>> GetByEmail(String information)
        {

            if (System.Net.Mail.MailAddress.TryCreate(information, out var mailAddress)) {

                var users = await _queryContext.AllUsers.ToListAsync();
                var usersViewModel = users
                    .Select(u => _mapper.Map<UserViewModel>(u))
                    .Where(u => u.Email.ToLower().Contains(information.ToLower()))
                    .ToList();
                return usersViewModel;
            }
            else
            {
                var users = await _queryContext.AllUsers.ToListAsync();
                var usersViewModel = users
                    .Select(u => _mapper.Map<UserViewModel>(u))
                    .Where(u => u.Person.Name.ToLower().Contains(information.ToLower()))
                    .ToList();
                return usersViewModel;
            }


        }

        public UserViewModel GetById(Guid id)
        {
            var user = _mapper.Map<UserViewModel>(_queryContext.AllUsers.Where(u => u.ID == id).First());
            return user;
        }

    }
}
