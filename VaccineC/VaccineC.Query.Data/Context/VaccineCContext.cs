using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Data.Context
{
    public class VaccineCContext : DbContext
    {
        public VaccineCContext(DbContextOptions<VaccineCContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonsPhysical> PersonsPhysical { get; set; }
        public DbSet<PersonsJuridical> PersonsJuridical { get; set; }
        public DbSet<PaymentForm> PaymentForms { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<UserResource> UsersResources { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyParameter> CompaniesParameters { get; set; }
        public DbSet<CompanySchedule> CompaniesSchedules { get; set; }
        public DbSet<PersonPhone> PersonsPhones { get; set; }
        public DbSet<PersonAddress> PersonsAddresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SbimVaccines> SbimVaccines { get; set; }
        public DbSet<ProductDoses> ProductsDoses { get; set; }
        public DbSet<ProductSummaryBatch> ProductsSummariesBatches { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<MovementProduct> MovementsProducts { get; set; }
        public DbSet<BudgetProduct> BudgetsProducts { get; set; }
        public DbSet<Authorization> Authorizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<PersonsPhysical>().ToTable("PersonsPhysical");
            modelBuilder.Entity<PersonsJuridical>().ToTable("PersonsJuridical");
            modelBuilder.Entity<PaymentForm>().ToTable("PaymentForms");
            modelBuilder.Entity<Resource>().ToTable("Resources");
            modelBuilder.Entity<UserResource>().ToTable("UsersResources");
            modelBuilder.Entity<Company>().ToTable("Companies");
            modelBuilder.Entity<CompanyParameter>().ToTable("CompaniesParameters");
            modelBuilder.Entity<CompanySchedule>().ToTable("CompaniesSchedules");
            modelBuilder.Entity<PersonPhone>().ToTable("PersonsPhones");
            modelBuilder.Entity<PersonAddress>().ToTable("PersonsAddresses");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<SbimVaccines>().ToTable("SbimVaccines");
            modelBuilder.Entity<ProductDoses>().ToTable("ProductsDoses");
            modelBuilder.Entity<ProductSummaryBatch>().ToTable("ProductsSummariesBatches");
            modelBuilder.Entity<Movement>().ToTable("Movements");
            modelBuilder.Entity<MovementProduct>().ToTable("MovementsProducts");
            modelBuilder.Entity<BudgetProduct>().ToTable("BudgetsProducts");
            modelBuilder.Entity<Authorization>().ToTable("Authorizations");

            base.OnModelCreating(modelBuilder);
        }
    }
}
