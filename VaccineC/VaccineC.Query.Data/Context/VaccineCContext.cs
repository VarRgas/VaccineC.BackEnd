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
        public DbSet<PaymentForm> PaymentForms { get; set; }
        public DbSet<Resource> Resources { get; set; }
<<<<<<< Updated upstream
        public DbSet<UserResource> UsersResources { get; set; }
=======
        public DbSet<Company> Companies { get; set; }
>>>>>>> Stashed changes

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<PaymentForm>().ToTable("PaymentForms");
            modelBuilder.Entity<Resource>().ToTable("Resources");
<<<<<<< Updated upstream
            modelBuilder.Entity<UserResource>().ToTable("UsersResources");
=======
            modelBuilder.Entity<Company>().ToTable("Companies");
>>>>>>> Stashed changes

            base.OnModelCreating(modelBuilder);
        }
    }
}
