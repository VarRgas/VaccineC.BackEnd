using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Data.QueryContext
{
    public class QueryContext : IQueryContext
    {
        private readonly VaccineCContext _context;

        public QueryContext(VaccineCContext context)
        {
            _context = context;
        }

        public IQueryable<Example> AllExamples
        {
            get
            {
                return _context
                .Set<Example>();
            }
        }

        public IQueryable<PaymentForm> AllPaymentForms
        {
            get
            {
                return _context
               .Set<PaymentForm>()
               .OrderBy(r => r.Name);
            }
        }

        public IQueryable<Resource> AllResources
        {
            get
            {
                return _context
               .Set<Resource>()
               .OrderBy(r => r.Name);
            }
        }

        public IQueryable<Company> AllCompanies
        {
            get
            {
                return _context
               .Set<Company>()
               .Include(r => r.Person)
               .OrderBy(r => r.Person.Name);
            }
        }

        public IQueryable<CompanyParameter> AllCompaniesParameters
        {
            get
            {
                return _context
               .Set<CompanyParameter>();
            }
        }

        public IQueryable<CompanySchedule> AllCompaniesSchedules
        {
            get
            {
                return _context
               .Set<CompanySchedule>()
               .OrderBy(r => r.Day);
            }
        }

        public IQueryable<User> AllUsers
        {
            get
            {
                return _context
               .Set<User>()
               .Include(r => r.Person)
               .OrderBy(r => r.Email);
            }
        }

        public IQueryable<Person> AllPersons
        {
            get
            {
                return _context
               .Set<Person>()
               .OrderBy(r => r.Name);
            }
        }

        public IQueryable<PersonPhone> AllPersonsPhones
        {
            get
            {
                return _context
               .Set<PersonPhone>()
               .OrderBy(r => r.CodeArea)
               .OrderBy(r => r.NumberPhone);
            }
        }

        public IQueryable<UserResource> AllUserResources
        {
            get
            {
                return _context
               .Set<UserResource>()
               .OrderBy(r => r.ID);
            }
        }

        public IQueryable<PersonAddress> AllPersonsAddresses
        {
            get
            {
                return _context
               .Set<PersonAddress>();
            }
        }

        public IQueryable<PersonsPhysical> AllPersonsPhysicals
        {
            get
            {
                return _context
               .Set<PersonsPhysical>();
            }
        }

        public IQueryable<PersonsJuridical> AllPersonsJuridicals
        {
            get
            {
                return _context
               .Set<PersonsJuridical>();
            }
        }

        public IQueryable<Product> AllProducts
        {
            get
            {
                return _context
               .Set<Product>()
               .Include(r => r.SbimVaccines)
               .OrderBy(r => r.Name);
            }
        }

        public IQueryable<ProductDoses> AllProductsDoses
        {
            get
            {
                return _context
               .Set<ProductDoses>()
               .OrderBy(r => r.DoseType);
            }
        }
    }
}
