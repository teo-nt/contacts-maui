using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.UseCases.Interfaces;
using System.Collections.ObjectModel;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels
{
    public partial class ContactsViewModel : ObservableObject
    {
        private readonly IViewContactsUseCase viewContactsUseCase;
        private readonly IDeleteContactUseCase deleteContactUseCase;

        public ObservableCollection<Contact> Contacts { get; set; } = [];

        public ContactsViewModel(IViewContactsUseCase viewContactsUseCase, IDeleteContactUseCase deleteContactUseCase)
        {
            this.viewContactsUseCase = viewContactsUseCase;
            this.deleteContactUseCase = deleteContactUseCase;
        }

        public async Task LoadContactsAsync()
        {

            Contacts.Clear();
            var contacts = await viewContactsUseCase.ExecuteAsync(string.Empty);
            if (contacts is not null && contacts.Count > 0)
            {
                foreach (var contact in contacts)
                {
                    Contacts.Add(contact);
                }
            }

        }

        [RelayCommand]
        public async Task DeleteContact(int contactId)
        {
            await deleteContactUseCase.ExecuteAsync(contactId);
            await LoadContactsAsync();
        }

    }
}
