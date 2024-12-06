using Contacts.UseCases.PluginInterfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.InMemory
{
    // All the code in this file is included in all platforms.
    public class ContactInMemoryRepository : IContactRepository
    {
        public static List<Contact> _contacts;

        public ContactInMemoryRepository()
        {
            _contacts = new List<Contact>()
            {
                new Contact { ContactId = 1, Name = "John Doe", Email = "john@test.com" },
                new Contact { ContactId = 2, Name = "Jane Doe", Email = "jane@test.com" },
                new Contact { ContactId = 3, Name = "Tom Hanks", Email = "tom@test.com" },
                new Contact { ContactId = 4, Name = "Frank Liu", Email = "frank@test.com" },
            };
        }

        public Task<List<Contact>> GetContactsAsync(string? filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return Task.FromResult(_contacts);
            }

            var contacts = _contacts.Where(c => c.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();

            if (contacts is null || contacts.Count == 0)
            {
                contacts = _contacts.Where(c => c.Email.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                return Task.FromResult(contacts);
            }

            if (contacts is null || contacts.Count == 0)
            {
                contacts = _contacts.Where(c => c.Phone.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                return Task.FromResult(contacts);
            }

            if (contacts is null || contacts.Count == 0)
            {
                contacts = _contacts.Where(c => c.Address.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                return Task.FromResult(contacts);
            }

            return Task.FromResult(contacts);
        }

        public Task<Contact>? GetContactByIdAsync(int contactId)
        {
            var contact = _contacts.FirstOrDefault(c => c.ContactId == contactId);
            if (contact != null)
            {
                return Task.FromResult(new Contact
                {
                    ContactId = contact.ContactId,
                    Name = contact.Name,
                    Email = contact.Email,
                    Phone = contact.Phone,
                    Address = contact.Address,
                });
            }
            return null;
        }

        public Task UpdateContactAsync(int contactId, Contact contact)
        {
            if (contactId != contact.ContactId) return Task.CompletedTask;

            var contactToUpdate = _contacts.FirstOrDefault(c => c.ContactId == contactId);
            if (contactToUpdate != null)
            {
                contactToUpdate.Address = contact.Address;
                contactToUpdate.Phone = contact.Phone;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Name = contact.Name;
            }
            return Task.CompletedTask;
        }

        public Task AddContactAsync(Contact contact)
        {
            var maxId = _contacts.Max(c => c.ContactId);
            contact.ContactId = maxId + 1;
            _contacts.Add(contact);

            return Task.CompletedTask;
        }

        public Task DeleteContactAsync(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.ContactId == id);
            if (contact is not null) _contacts.Remove(contact);

            return Task.CompletedTask;
        }
    }
}
