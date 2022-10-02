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
               .Set<CompanyParameter>()
               .Include(r => r.PaymentForm);
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

        public IQueryable<ProductSummaryBatch> AllProductsSummariesBatches
        {
            get
            {
                return _context
               .Set<ProductSummaryBatch>()
               .Include(r => r.Products)
               .OrderBy(r => r.ValidityBatchDate);
            }
        }

        public IQueryable<Movement> AllMovements
        {
            get
            {
                return _context
               .Set<Movement>()
               .OrderByDescending(r => r.Situation)
               .ThenByDescending(r => r.MovementNumber);
            }
        }

        public IQueryable<MovementProduct> AllMovementsProducts
        {
            get
            {
                return _context
               .Set<MovementProduct>()
               .Include(r => r.Product);
            }
        }

        public IQueryable<BudgetProduct> AllBudgetsProducts
        {
            get
            {
                return _context
               .Set<BudgetProduct>()
               .Include(r => r.Product)
               .Include(r => r.Person)
               .OrderBy(r => r.Person.Name)
                .ThenBy(r => r.Product.Name)
                .ThenBy(r => r.ProductDose);
            }
        }
        public IQueryable<Budget> AllBudgets
        {
            get
            {
                return _context
               .Set<Budget>()
               .Include(u => u.Users)
               .Include(p => p.Persons)
               .OrderByDescending(r => r.Register);
            }
        }

        public IQueryable<Authorization> AllAuthorizations
        {
            get
            {
                return _context
               .Set<Authorization>()
               .Include(bp => bp.BudgetProduct)
                .ThenInclude(p => p.Product)
               .OrderBy(r => r.AuthorizationNumber);
            }
        }

        public IQueryable<Notification> AllNotifications
        {
            get
            {
                return _context
               .Set<Notification>()
               .OrderByDescending(r => r.Register);
            }
        }

        public IQueryable<BudgetNegotiation> AllBudgetsNegotiations
        {
            get
            {
                return _context
               .Set<BudgetNegotiation>()
               .Include(bn => bn.PaymentForm)
               .OrderBy(bn => bn.PaymentForm.Name);
            }
        }

        public IQueryable<Discard> AllDiscards
        {
            get
            {
                return _context
               .Set<Discard>()
               .Include(d => d.ProductSummaryBatch)
                .ThenInclude(psb => psb.Products)
               .Include(d => d.User)
                .ThenInclude(u => u.Person)   
               .OrderByDescending(d => d.Register);
            }
        }

        public IQueryable<BudgetHistoric> AllBudgetsHistorics
        {
            get
            {
                return _context
               .Set<BudgetHistoric>()
               .Include(bh => bh.User)
                .ThenInclude(u => u.Person)
               .OrderByDescending(bh => bh.Register);
            }
        }
    }
}
