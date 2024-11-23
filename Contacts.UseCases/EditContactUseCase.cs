using Contact = Contacts.CoreBusiness.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.UseCases.PluginInterfaces;
using Contacts.UseCases.Interfaces;

namespace Contacts.UseCases
{
    public class EditContactUseCase : IEditContactUseCase
    {
        private readonly IContactRepository contactRepository;

        public EditContactUseCase(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task ExecuteAsync(int contactId, Contact contact)
        {
            await this.contactRepository.UpdateContactAsync(contactId, contact);
        }
    }
}
