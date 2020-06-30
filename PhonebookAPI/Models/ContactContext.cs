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

        public DbSet<Contact> Contacts { get; set; }
    }
}
