using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class PersonJuridicalAppService : IPersonJuridicalAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public PersonJuridicalAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonsJuridicalViewModel>> GetAllAsync()
        {
            var personsJuridicals = await _queryContext.AllPersonsJuridicals.ToListAsync();
            var personJuridicalViewModel = personsJuridicals.Select(r => _mapper.Map<PersonsJuridicalViewModel>(r)).ToList();
            return personJuridicalViewModel;
        }

        public async Task<IEnumerable<PersonsJuridicalViewModel>> GetAllJuridicalComplementsByPersonId(Guid personId)
        {
            var personsJuridicals = await _queryContext.AllPersonsJuridicals.Where(pp => pp.PersonID == personId).ToListAsync();
            var personsJuridicalsViewModel = personsJuridicals.Select(r => _mapper.Map<PersonsJuridicalViewModel>(r)).ToList();
            return personsJuridicalsViewModel;
        }
    }
}
