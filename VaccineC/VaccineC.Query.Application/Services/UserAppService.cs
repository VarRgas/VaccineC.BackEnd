using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;


namespace VaccineC.Query.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public UserAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
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

        public async Task<Boolean> GetUserPermission(Guid id, string currentUrl)
        {
            var userResources = (from ur in _context.UsersResources
                                 join r in _context.Resources on ur.ResourcesId equals r.ID
                                 where ur.UsersId.Equals(id)
                                 where r.UrlName.ToLower().Contains(currentUrl.ToLower())
                                 select r).FirstOrDefault();

            if (userResources != null)
            {
                return true;
            }

            return false;
        }
    }
}
