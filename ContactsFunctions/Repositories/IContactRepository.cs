using ContactsFunctions.Models;

namespace ContactsFunctions.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetContactsAsync();
        Task AddAsync(Contact contact);
    }
}
