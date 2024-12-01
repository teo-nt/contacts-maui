using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Views_Mvvm;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        private readonly IViewContactUseCase viewContactUseCase;
        private readonly IEditContactUseCase editContactUseCase;

        [ObservableProperty]
        private Contact _contact = new Contact();

        public ContactViewModel(IViewContactUseCase viewContactUseCase, IEditContactUseCase editContactUseCase)
        {
            this.viewContactUseCase = viewContactUseCase;
            this.editContactUseCase = editContactUseCase;
        }

        public async Task LoadContact(int contactId)
        {
            Contact = await viewContactUseCase.ExecuteAsync(contactId) ?? new Contact();
        }

        [RelayCommand]
        public async Task EditContact()
        {
            await editContactUseCase.ExecuteAsync(Contact.ContactId, Contact);
            await Shell.Current.GoToAsync($"//{nameof(ContactsMVVMPage)}");
        }

        [RelayCommand]
        public async Task BackToContacts()
        {
            await Shell.Current.GoToAsync($"//{nameof(ContactsMVVMPage)}");
        }
    }
}
