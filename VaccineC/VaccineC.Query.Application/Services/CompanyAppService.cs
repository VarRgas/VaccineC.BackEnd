using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Application.Services
{
    public class CompanyAppService : ICompanyAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public CompanyAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<CompanyViewModel>> GetAllAsync()
        {
            var companies = await _queryContext.AllCompanies.ToListAsync();
            var companiesViewModel = companies.Select(r => _mapper.Map<CompanyViewModel>(r)).ToList();
            return companiesViewModel;
        }

        public async Task <CompaniesParametersViewModel> GetAllParametersByCompanyID(Guid companyId)
        {
            var companiesParameters = await _queryContext.AllCompaniesParameters.Where(ur => ur.CompanyId == companyId).ToListAsync();
            var companyParameter = companiesParameters.Select(r => _mapper.Map<CompaniesParametersViewModel>(r)).FirstOrDefault();
            return companyParameter;
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

        public CompanyViewModel GetFirst()
        {
            var company = _mapper.Map<CompanyViewModel>(_queryContext.AllCompanies.First());
            return company;
        }

        public CompaniesParametersViewModel GetCompanyParameterByCompanyId(Guid companyId)
        {
            var companyParameter = _mapper.Map<CompaniesParametersViewModel>(_queryContext.AllCompaniesParameters.Where(r => r.CompanyId == companyId).First());
            return companyParameter;
        }

        public List<TimeSpan> GetMinMaxCompanySchedule(Guid companyId)
        {

            List<TimeSpan> listTime = new List<TimeSpan>();

            List<CompanySchedule> companiesSchedules = (from cs in _context.CompaniesSchedules
                                                                 where cs.CompanyId == companyId
                                                                 select cs).ToList();

            if (companiesSchedules.Count() > 0) {
            

                TimeSpan minValue = TimeSpan.MaxValue;
                TimeSpan maxValue = TimeSpan.MinValue;

                foreach (CompanySchedule companiesSchedule in companiesSchedules)
                {
                    if(TimeSpan.Compare(companiesSchedule.StartTime, minValue) == -1)
                    {
                        minValue = companiesSchedule.StartTime;
                    }

                    if(TimeSpan.Compare(companiesSchedule.FinalTime, maxValue) == 1)
                    {
                        maxValue = companiesSchedule.FinalTime;
                    }

                }
                               
                listTime.Add(minValue);
                listTime.Add(maxValue + TimeSpan.FromMinutes(1));
            }


            return listTime;
        }
    }
}
