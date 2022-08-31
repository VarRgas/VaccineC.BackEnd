using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;


namespace VaccineC.Query.Application.Services
{
    public class CompanyParameterAppService : ICompanyParameterAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public CompanyParameterAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompaniesParametersViewModel>> GetAllAsync()
        {
            var companiesParameters = await _queryContext.AllCompaniesParameters.ToListAsync();
            var companiesParametersViewModel = companiesParameters.Select(r => _mapper.Map<CompaniesParametersViewModel>(r)).ToList();
            return companiesParametersViewModel;
        }

        public CompaniesParametersViewModel GetById(Guid id)
        {
            var companyParameter = _mapper.Map<CompaniesParametersViewModel>(_queryContext.AllCompaniesParameters.Where(r => r.ID == id).First());
            return companyParameter;
        }
    }
}
