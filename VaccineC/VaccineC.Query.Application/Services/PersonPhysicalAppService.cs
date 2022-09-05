using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;


namespace VaccineC.Query.Application.Services
{
    public class PersonPhysicalAppService : IPersonPhysicalAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public PersonPhysicalAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonsPhysicalViewModel>> GetAllAsync()
        {
            var personsPhysicals = await _queryContext.AllPersonsPhysicals.ToListAsync();
            var personPhysicalViewModel = personsPhysicals.Select(r => _mapper.Map<PersonsPhysicalViewModel>(r)).ToList();
            return personPhysicalViewModel;
        }

        public async Task<IEnumerable<PersonsPhysicalViewModel>> GetAllPhysicalComplementsByPersonId(Guid personId)
        {
            var personsPhysicals = await _queryContext.AllPersonsPhysicals.Where(pp => pp.PersonID == personId).ToListAsync();
            var personsPhysicalsViewModel = personsPhysicals.Select(r => _mapper.Map<PersonsPhysicalViewModel>(r)).ToList();
            return personsPhysicalsViewModel;
        }
    }
}
