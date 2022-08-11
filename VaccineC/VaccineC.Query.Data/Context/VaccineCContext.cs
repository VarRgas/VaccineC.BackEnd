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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Example>().ToTable("Customers");
            modelBuilder.Entity<User>().ToTable("users");

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=CXJ0975\\SQLEXPRESS;Initial Catalog=vaccinec;User ID=sa;Password=PromobSQL2021;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //}

    }
}
