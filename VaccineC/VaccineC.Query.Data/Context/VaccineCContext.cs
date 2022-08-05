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


        public DbSet<Example> Guests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Example>().ToTable("Customers");

            base.OnModelCreating(modelBuilder);
        }
    }
}
