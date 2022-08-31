using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;
using VaccineC.Query.Model.Models;


namespace VaccineC.Query.Application.Services
{
    public class PersonAppService : IPersonAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;


        public PersonAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;

        }

        public async Task<IEnumerable<PersonViewModel>> GetAllAsync()
        {
            var persons = await _queryContext.AllPersons.ToListAsync();
            var personsViewModel = persons.Select(p => _mapper.Map<PersonViewModel>(p)).ToList();
            return personsViewModel;
        }

        public async Task<IEnumerable<PersonViewModel>> GetAllByType(string personType)
        {
            var persons = await _queryContext.AllPersons.ToListAsync();
            var personsViewModel = persons
                .Select(p => _mapper.Map<PersonViewModel>(p))
                .Where(p => p.PersonType.Equals(personType))
                .ToList();
            return personsViewModel;
        }

        public Task<IEnumerable<PersonViewModel>> GetAllUserAutocomplete()
        {

            List<Person> persons = (from p in _context.Persons
                                    join u in _context.Users on p.ID equals u.PersonId into _u
                                    from x in _u.DefaultIfEmpty()
                                    where p.PersonType.Equals("F")
                                    where x.PersonId.Equals(null)
                                    select p).ToList();

            var response = _mapper.Map<IEnumerable<PersonViewModel>>(persons);

            return Task.FromResult(response);
        }

        public Task<IEnumerable<PersonViewModel>> GetAllCompanyAutocomplete()
        {

            List<Person> persons = (from p in _context.Persons
                                    join c in _context.Companies on p.ID equals c.PersonId into _c
                                    from x in _c.DefaultIfEmpty()
                                    where p.PersonType.Equals("J")
                                    where x.PersonId.Equals(null)
                                    select p).ToList();

            var response = _mapper.Map<IEnumerable<PersonViewModel>>(persons);

            return Task.FromResult(response);
        }

    }
}
