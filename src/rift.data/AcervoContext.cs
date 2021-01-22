using Microsoft.EntityFrameworkCore;
using rift.domain;

namespace rift.data
{
    public class AcervoContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Phone> Phones { get; set; }

        public AcervoContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Person>()
                .HasIndex(u => u.Document)
                .IsUnique();

            builder.Entity<Person>()
               .HasIndex(u => u.CPF)
               .IsUnique();

            builder.Entity<Company>()
               .HasIndex(u => u.CNPJ)
               .IsUnique();

            builder.Entity<Company>()
               .HasIndex(u => u.CompanyName)
               .IsUnique();

            builder.Entity<Company>()
              .HasIndex(u => u.FantasyName)
              .IsUnique();
        }
    }

}
