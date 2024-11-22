using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Maui.Models
{
    public static class ContactRepository
    {
        public static List<Contact> _contacts = new List<Contact>()
        {
            new Contact { ContactId = 1, Name = "John Doe", Email = "john@test.com" },
            new Contact { ContactId = 2, Name = "Jane Doe", Email = "jane@test.com" },
            new Contact { ContactId = 3, Name = "Tom Hanks", Email = "tom@test.com" },
            new Contact { ContactId = 4, Name = "Frank Liu", Email = "frank@test.com" },
        };

        public static List<Contact> GetContacts() => _contacts;

        public static Contact? GetContactById(int id) {
            var contact = _contacts.FirstOrDefault(c => c.ContactId == id);
            if (contact != null)
            {
                return new Contact
                {
                    ContactId = contact.ContactId,
                    Name = contact.Name,
                    Email = contact.Email,
                    Phone = contact.Phone,
                    Address = contact.Address,
                };
            }
            return null;
        }

        public static void UpdateContact(int contactId, Contact contact)
        {
            if (contactId != contact.ContactId) return;

            var contactToUpdate = _contacts.FirstOrDefault(c => c.ContactId == contactId);
            if (contactToUpdate != null)
            {
                contactToUpdate.Address = contact.Address;
                contactToUpdate.Phone = contact.Phone;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Name = contact.Name;
            }     
        }

        public static void AddContact(Contact contact)
        {
            var maxId = _contacts.Max(c => c.ContactId);
            contact.ContactId = maxId + 1;
            _contacts.Add(contact);
        }

        public static void DeleteContact(int contactId)
        {
            var contact = _contacts.FirstOrDefault(c => c.ContactId == contactId);
            if (contact is not null) _contacts.Remove(contact);
        }

        public static List<Contact> SearchContacts(string filterText)
        {
            var contacts = _contacts.Where(c => c.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();

            if (contacts is null || contacts.Count == 0)
            {
                contacts = _contacts.Where(c => c.Email.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                return contacts;
            }

            if (contacts is null || contacts.Count == 0)
            {
                contacts = _contacts.Where(c => c.Phone.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                return contacts;
            }

            if (contacts is null || contacts.Count == 0)
            {
                contacts = _contacts.Where(c => c.Address.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                return contacts;
            }

            return contacts;
        }
    }
}
