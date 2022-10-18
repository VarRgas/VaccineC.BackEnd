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

        public async Task<IEnumerable<PersonViewModel>> GetAllAuthorizationAutocomplete()
        {
            var persons = await _queryContext.AllPersons.ToListAsync();
            var personsViewModel = persons
                .Select(p => _mapper.Map<PersonViewModel>(p))
                .ToList();

            foreach(var person in personsViewModel)
            {
                var personPhone = (from pp in _context.PersonsPhones
                                   where pp.PhoneType.Equals("P") && pp.PersonID.Equals(person.ID)
                                   select pp).FirstOrDefault();

                var personPhoneViewModel = _mapper.Map<PersonPhoneViewModel>(personPhone);
                person.PersonPrincipalPhone = personPhoneViewModel;


                var personAddress = (from pa in _context.PersonsAddresses
                                     where pa.AddressType.Equals("P") && pa.PersonID.Equals(person.ID)
                                     select pa).FirstOrDefault();

                var personAddressViewModel = _mapper.Map<PersonAddressViewModel>(personAddress);
                person.PersonPrincipalAddress = personAddressViewModel;
            }

            return personsViewModel;
        }

        public Task<IEnumerable<PersonViewModel>> GetByName(String information)
        {


            long n;
            bool isNumeric = long.TryParse(information, out n);

            if (isNumeric)
            {
                if (information.Length <= 11) {
                    List<Person> persons = (from p in _context.Persons
                                            join c in _context.PersonsPhysical on p.ID equals c.PersonID into _c
                                            from x in _c.DefaultIfEmpty()
                                            where x.CpfNumber.Contains(information)
                                            select p).ToList();

                    foreach (var person in persons)
                    {
                        if (person.PersonType.Equals("F"))
                        {
                            var personPhysical = (from pf in _context.PersonsPhysical
                                                  where pf.PersonID.Equals(person.ID)
                                                  select pf).FirstOrDefault();

                            if (personPhysical != null)
                            {
                                person.PersonsPhysical = personPhysical;
                            }

                        }

                    }

                    var response = _mapper.Map<IEnumerable<PersonViewModel>>(persons);

                    return Task.FromResult(response);
                }
                else
                {
                    List<Person> persons = (from p in _context.Persons
                                            join c in _context.PersonsJuridical on p.ID equals c.PersonID into _c
                                            from x in _c.DefaultIfEmpty()
                                            where x.CnpjNumber.Contains(information)
                                            select p).ToList();

                    foreach (var person in persons)
                    {
                        if (person.PersonType.Equals("F"))
                        {
                            var personPhysical = (from pf in _context.PersonsPhysical
                                                  where pf.PersonID.Equals(person.ID)
                                                  select pf).FirstOrDefault();

                            if (personPhysical != null)
                            {
                                person.PersonsPhysical = personPhysical;
                            }

                        }

                    }

                    var response = _mapper.Map<IEnumerable<PersonViewModel>>(persons);

                    return Task.FromResult(response);
                }

            }
            else
            {
                List<Person> persons = (from p in _context.Persons
                                        where p.Name.ToLower().Contains(information.ToLower())
                                        select p).ToList();

                foreach (var person in persons)
                {
                    if (person.PersonType.Equals("F"))
                    {
                        var personPhysical = (from pf in _context.PersonsPhysical
                                              where pf.PersonID.Equals(person.ID)
                                              select pf).FirstOrDefault();

                        if (personPhysical != null)
                        {
                            person.PersonsPhysical = personPhysical;
                        }

                    }

                }
                var response = _mapper.Map<IEnumerable<PersonViewModel>>(persons);
                return Task.FromResult(response);
            }



        }

    }
}
