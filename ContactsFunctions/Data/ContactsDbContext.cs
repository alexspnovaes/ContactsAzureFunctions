using ContactsFunctions.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsFunctions.Data
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
