using ContactsFunctions.Models;
using ContactsFunctions.Repositories;

namespace ContactsFunctions.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _contactRepository.GetContactsAsync();
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            var contactInsert = new Contact
            {
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                Ddd = contact.Ddd
            };

            await _contactRepository.AddAsync(contactInsert);
            return contactInsert;
        }
    }
}
