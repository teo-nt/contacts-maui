﻿

using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.UseCases.PluginInterfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetContactsAsync(string? filterText);
        Task<Contact?> GetContactByIdAsync(int contactId);
        Task UpdateContactAsync(int contactId, Contact contact);
        Task AddContactAsync(Contact contact);
        Task DeleteContactAsync(int id);
    }
}
