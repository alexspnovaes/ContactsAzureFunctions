using ContactsFunctions.Models;

namespace ContactsFunctions.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetContactsAsync();
        Task<Contact> CreateContactAsync(Contact contact);
    }
}
