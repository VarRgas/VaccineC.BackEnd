using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;


namespace VaccineC.Query.Application.Services
{
    public class ResourceAppService : IResourceAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public ResourceAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResourceViewModel>> GetAllAsync()
        {
            var resources = await _queryContext.AllResources.ToListAsync();
            var resourcesViewModel = resources.Select(r => _mapper.Map<ResourceViewModel>(r)).ToList();
            return resourcesViewModel;
        }

        public async Task<IEnumerable<ResourceViewModel>> GetByName(String name)
        {

            var resources = await _queryContext.AllResources.ToListAsync();
            var resourcesViewModel = resources
                .Select(r => _mapper.Map<ResourceViewModel>(r))
                .Where(r => r.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
            return resourcesViewModel;

        }

        public async Task<IEnumerable<ResourceViewModel>> GetByUserResource(Guid userId)
        {

            var userResources = await _queryContext.AllUserResources.Where(ur => ur.UsersId == userId).ToListAsync();

            List<Guid> listResourcesId = new List<Guid>();

            foreach (var userResource in userResources)
            {
                listResourcesId.Add(userResource.ResourcesId);
            }

            var resources = await _queryContext.AllResources
                .Where(r => listResourcesId.Contains(r.ID)).ToListAsync();

            var resourcesViewModel = resources.Select(r => _mapper.Map<ResourceViewModel>(r)).ToList();
            return resourcesViewModel;

        }

        public ResourceViewModel GetById(Guid id)
        {
            var resource = _mapper.Map<ResourceViewModel>(_queryContext.AllResources.Where(r => r.ID == id).First());
            return resource;
        }
    }
}
