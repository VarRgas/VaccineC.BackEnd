using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class UserResourceAppService : IUserResourceAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public UserResourceAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResourceViewModel>> GetAllAsync()
        {
            var usersResources = await _queryContext.AllUserResources.ToListAsync();
            var usersResourcesViewModel = usersResources.Select(u => _mapper.Map<UserResourceViewModel>(u)).ToList();
            return usersResourcesViewModel;
        }

        public async Task<IEnumerable<UserResourceViewModel>> GetAllByUser(Guid userId)
        {
            var usersResources = await _queryContext.AllUserResources.ToListAsync();
            var usersResourcesViewModel = usersResources
                .Select(ur => _mapper.Map<UserResourceViewModel>(ur))
                .Where(ur => ur.UsersId.Equals(userId))
                .ToList();
            return usersResourcesViewModel;
        }
    }
}
