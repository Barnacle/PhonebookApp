using Microsoft.EntityFrameworkCore;

// ReSharper disable once IdentifierTypo
namespace PhonebookAPI.Models
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Number>()
                .HasIndex(p => new { p.PhoneNumber })
                .IsUnique();
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Number> Numbers { get; set; }
    }
}
