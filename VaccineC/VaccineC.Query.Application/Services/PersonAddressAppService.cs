using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class PersonAddressAppService : IPersonAddressAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public PersonAddressAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonAddressViewModel>> GetAllAsync()
        {
            var personAddresses = await _queryContext.AllPersonsAddresses.ToListAsync();
            var personAddressesViewModel = personAddresses.Select(r => _mapper.Map<PersonAddressViewModel>(r)).ToList();
            return personAddressesViewModel;
        }

        public async Task<IEnumerable<PersonAddressViewModel>> GetAllPersonsAddressesByPersonId(Guid personId)
        {
            var personAddresses = await _queryContext.AllPersonsAddresses.Where(pa => pa.PersonID == personId).ToListAsync();
            var personAddressesViewModel = personAddresses.Select(r => _mapper.Map<PersonAddressViewModel>(r)).ToList();
            return personAddressesViewModel;
        }

        public PersonAddressViewModel GetById(Guid id)
        {
            var personAddress = _mapper.Map<PersonAddressViewModel>(_queryContext.AllPersonsAddresses.Where(r => r.ID == id).First());
            return personAddress;
        }
    }
}
