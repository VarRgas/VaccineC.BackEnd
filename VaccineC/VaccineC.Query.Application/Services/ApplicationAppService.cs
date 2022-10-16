using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class ApplicationAppService : IApplicationAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public ApplicationAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ApplicationViewModel>> GetAllAsync()
        {
            var applications = await _queryContext.AllApplications.ToListAsync();
            var applicationsViewModel = applications.Select(r => _mapper.Map<ApplicationViewModel>(r)).ToList();
            return applicationsViewModel;
        }

        public ApplicationViewModel GetById(Guid id)
        {
            var application = _mapper.Map<ApplicationViewModel>(_queryContext.AllApplications.Where(r => r.ID == id).First());
            return application;
        }

        public async Task<IEnumerable<ApplicationViewModel>> GetByName(string name)
        {
            var applications = await _queryContext.AllApplications.ToListAsync();
            var applicationsViewModel = applications
                .Select(r => _mapper.Map<ApplicationViewModel>(r)).ToList();
            return applicationsViewModel;
        }
    }
}
