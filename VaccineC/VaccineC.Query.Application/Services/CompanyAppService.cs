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

        public async Task<IEnumerable<CompaniesParametersViewModel>> GetAllParametersByCompanyID(Guid id)
        {
            var companiesParams = await _queryContext.AllCompaniesParameters.Where(ur => ur.CompanyId == id).ToListAsync();

            List<Guid> listCompaniesId = new List<Guid>();

            foreach (var item in companiesParams)
            {
                listCompaniesId.Add(item.CompanyId);
            }

            var companies = await _queryContext.AllResources.Where(r => listCompaniesId.Contains(r.ID)).ToListAsync();

            var companiesViewModel = companies.Select(r => _mapper.Map<CompaniesParametersViewModel>(r)).ToList();
            return companiesViewModel;
        }

        public async Task<IEnumerable<CompanyViewModel>> GetByName(String name)
        {

            var companies = await _queryContext.AllCompanies.ToListAsync();
            var companiesViewModel = companies
                .Select(r => _mapper.Map<CompanyViewModel>(r))
                .Where(r => r.Person.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return companiesViewModel;

        }

        public CompanyViewModel GetById(Guid id)
        {
            var company = _mapper.Map<CompanyViewModel>(_queryContext.AllCompanies.Where(r => r.ID == id).First());
            return company;
        }
    }
}
