using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Context
{
    public class VaccineCCommandContext : DbContext
    {
        public VaccineCCommandContext(DbContextOptions<VaccineCCommandContext> options)
            : base(options)
        {

        }

        public DbSet<Example> Example { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Person>().ToTable("Persons");

            base.OnModelCreating(modelBuilder);
        }
    }
}
