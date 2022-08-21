using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ResourceViewModel GetById(Guid id)
        {
            var resource = _mapper.Map<ResourceViewModel>(_queryContext.AllResources.Where(r => r.ID == id).First());
            return resource;
        }
    }
}
