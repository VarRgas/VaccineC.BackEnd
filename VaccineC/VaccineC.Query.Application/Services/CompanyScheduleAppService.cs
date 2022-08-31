using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class CompanyScheduleAppService : ICompanyScheduleAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public CompanyScheduleAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyScheduleViewModel>> GetAllAsync()
        {
            var companiesSchedules = await _queryContext.AllCompaniesSchedules.ToListAsync();
            var companiesSchedulesViewModel = companiesSchedules.Select(r => _mapper.Map<CompanyScheduleViewModel>(r)).ToList();
            return companiesSchedulesViewModel;
        }

        public CompanyScheduleViewModel GetById(Guid id)
        {
            var companySchedule = _mapper.Map<CompanyScheduleViewModel>(_queryContext.AllCompaniesSchedules.Where(r => r.ID == id).First());
            return companySchedule;
        }
    }
}
