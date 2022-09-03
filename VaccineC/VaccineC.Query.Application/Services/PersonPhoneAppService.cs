using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class PersonPhoneAppService : IPersonPhoneAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public PersonPhoneAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonPhoneViewModel>> GetAllAsync()
        {
            var personPhones = await _queryContext.AllPersonsPhones.ToListAsync();
            var personPhonesViewModel = personPhones.Select(r => _mapper.Map<PersonPhoneViewModel>(r)).ToList();
            return personPhonesViewModel;
        }

        public async Task<IEnumerable<PersonPhoneViewModel>> GetAllPersonsPhonesByPersonId(Guid personId)
        {
           var personsPhones = await _queryContext.AllPersonsPhones.Where(pp => pp.PersonID == personId).ToListAsync();
           var personsPhonesViewModel = personsPhones.Select(r => _mapper.Map<PersonPhoneViewModel>(r)).ToList();
           return personsPhonesViewModel;
        }

        public PersonPhoneViewModel GetById(Guid id)
        {
            var personPhone = _mapper.Map<PersonPhoneViewModel>(_queryContext.AllPersonsPhones.Where(r => r.ID == id).First());
            return personPhone;
        }
    }
}
