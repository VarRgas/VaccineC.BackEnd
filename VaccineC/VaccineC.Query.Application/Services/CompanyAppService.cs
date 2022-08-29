using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class CompanyAppService : ICompanyAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public CompanyAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyViewModel>> GetAllAsync()
        {
            var companies = await _queryContext.AllCompanies.ToListAsync();
            var companiesViewModel = companies.Select(r => _mapper.Map<CompanyViewModel>(r)).ToList();
            return companiesViewModel;
        }

        public async Task<IEnumerable<CompanyViewModel>> GetByName(String name)
        {

            var companies = await _queryContext.AllCompanies.ToListAsync();
            var companiesViewModel = companies
                .Select(r => _mapper.Map<CompanyViewModel>(r)).ToList();
            return companiesViewModel;

        }

        public CompanyViewModel GetById(Guid id)
        {
            var company = _mapper.Map<CompanyViewModel>(_queryContext.AllCompanies.Where(r => r.ID == id).First());
            return company;
        }
    }
}
